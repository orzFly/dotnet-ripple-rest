using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Windows.Forms;
using System.Collections;

namespace RippleRest.Demo
{
    /// <summary>
    /// Interaction logic for StupidPropertyGrid.xaml
    /// </summary>
    public partial class StupidPropertyGrid : System.Windows.Controls.UserControl
    {
        private ListBox listBox = new ListBox();
        private SplitContainer split = new SplitContainer();
        
        public StupidPropertyGrid()
        {
            InitializeComponent();
            PropertyGrid = new PropertyGrid();

            PropertyGrid.ToolbarVisible = false;
            PropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            PropertyGrid.HelpVisible = false;
            PropertyGrid.Dock = DockStyle.Fill;
            System.Drawing.Graphics a;

            listBox.Dock = DockStyle.Fill;
            listBox.IntegralHeight = true;
            split.Panel1.Controls.Add(listBox);
            split.Orientation = Orientation.Vertical;

            listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
        }

        void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PropertyGrid.SelectedObject = this.listBox.SelectedItem;
        }

        public PropertyGrid PropertyGrid
        {
            get;
            protected set;
        }

        public object SelectedObject
        {
            get
            {
                return PropertyGrid.SelectedObject;
            }
            set
            {
                if (value is IEnumerable)
                {
                    this.listBox.Items.Clear();
                    var e = value as IEnumerable;
                    foreach (var item in e)
	                {
                        this.listBox.Items.Add(item);
	                }
                    if (split.Panel2.Controls.Count == 0)
                        split.Panel2.Controls.Add(PropertyGrid);

                    this.Host.Child = split;

                    PropertyGrid.SelectedObject = null;
                    if (this.listBox.Items.Count > 0)
                        this.listBox.SelectedIndex = 0;


                    PropertyGrid.Dock = DockStyle.Fill;
                    listBox.Dock = DockStyle.Fill;
                }
                else
                {
                    this.Host.Child = PropertyGrid;
                    PropertyGrid.SelectedObject = value;
                }
            }
        }
    }
}
