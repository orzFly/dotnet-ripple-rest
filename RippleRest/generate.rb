require 'json'
require 'stringio'
require 'cgi'

class String
  def underscore
    self.gsub(/::/, '/').
    gsub(/([A-Z]+)([A-Z][a-z])/,'\1_\2').
    gsub(/([a-z\d])([A-Z])/,'\1_\2').
    tr("-", "_").
    downcase
  end
  def camel_case
    return self if self !~ /_/ && self =~ /[A-Z]+.*/
    split('_').map{|e| e.capitalize}.join.gsub("Xrp","XRP")
  end
  def camel_case_lower
    self.split('_').inject([]){ |buffer,e| buffer.push(buffer.empty? ? e : e.capitalize) }.join.gsub("Xrp","XRP")
  end
end

schemas = {}
Dir["json/*.json"].each do |i|
  key = File.basename(i, ".json")
  schemas[key] = JSON.parse File.read i
end

arraylist = []
schemas.values.select{|i|i["type"] == "object"}.each do |json|
  key = json["title"]
  
  io = open "#{key}.generated.cs", "w"
  io2 = StringIO.new
  json["properties"].each do |k, v|
    type = ""
    pattern = nil
    if v["type"] == "string" && v["pattern"]
      type = "string"
      pattern = v["pattern"]
    elsif v["type"] == "string"
      type = "string"
    elsif v["type"] == "boolean"
      type = "bool"
    elsif v["type"] == "array"
      type = "List<#{v["items"]["$ref"]}>"
    elsif v["type"] == "float"
      type = "double"
    elsif v["$ref"] == "UINT32"
      type = "UInt32"
    elsif v["$ref"] == "Timestamp"
      type = "DateTime"
    elsif v["$ref"] == "URL"
      type = "string"
    elsif v["$ref"] && (!schemas[v["$ref"]] || schemas[v["$ref"]]["type"] == "string")
      type = "string"
      pattern = schemas[v["$ref"]]["pattern"]
    elsif v["$ref"]
      type = "#{v["$ref"]}"
    elsif
      raise "Unsupported type"
    end
    io2.puts <<EOF
        /// <summary>
        /// #{v["description"]}
        /// </summary>
EOF
    io2.puts <<EOF if !pattern.nil?
        /// <remarks>
        /// This field should follow the following regular expression pattern:
        /// <code language="RegExp">#{CGI::escapeHTML(pattern)}</code>
        /// </remarks>
        [RegexpPattern(#{pattern.inspect})]
EOF
    io2.puts <<EOF
        [JsonProperty(#{k.inspect}#{(json["required"] || []).include?(k) ? ", Required = Required.Always" : ""})]
        public #{type} #{k.camel_case} { get; set; }

EOF
  end
  
io.puts <<EOF
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    /// <summary>
    /// #{json['description']}
    /// </summary>
	[Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public partial class #{key} : RestObject
    {
#{io2.string}
    }
}

EOF
  io.close
end