using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RippleRest.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RippleRestClient client = null;

        Account.QueryPaymentsOptions queryOptions = new Account.QueryPaymentsOptions();
        Payment payment = new Payment();

        public MainWindow()
        {
            InitializeComponent();

            this.QueryPaymentsOptionsBox.SelectedObject = queryOptions;

            payment.DestinationAmount = new Amount();
            payment.SourceAmount = new Amount();
            this.SubmitPaymentOptionsBox.SelectedObject = payment;
        }

        private void EndpointURL_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (client == null) 
                client = new RippleRestClient(this.EndpointURL.Text);

            client.EndpointURL = this.EndpointURL.Text;
        }

        private void IsServerConnected(object sender, RoutedEventArgs e)
        {
            this.IsServerConnectedButton.IsEnabled = false;
            this.IsServerConnectedLabel.Content = "Checking...";
            this.IsServerConnectedLabel.Foreground = Brushes.Orange;

            new Thread(() =>
                {
                    bool result = false;
                    Exception ex = null;
                    try
                    {
                        result = client.IsServerConnected();
                    }
                    catch (Exception exc)
                    {
                        ex = exc;
                    }

                    this.Dispatcher.Invoke((Action)delegate
                    {
                        this.IsServerConnectedButton.IsEnabled = true;
                        if (ex != null)
                        {
                            this.IsServerConnectedLabel.Content = ex.ToString();
                            this.IsServerConnectedLabel.Foreground = Brushes.Red;
                        }
                        else
                        {
                            if (result)
                            {
                                this.IsServerConnectedLabel.Content = "Server is connected to rippled.";
                                this.IsServerConnectedLabel.Foreground = Brushes.DarkGreen;
                            }
                            else
                            {
                                this.IsServerConnectedLabel.Content = "Server is not connected to rippled.";
                                this.IsServerConnectedLabel.Foreground = Brushes.Red;
                            }
                        }
                    });
                }).Start();
        }


        private void GetServerInfo(object sender, RoutedEventArgs e)
        {
            this.GetServerInfoButton.IsEnabled = false;
            this.GetServerInfoBox.SelectedObject = null;

            new Thread(() =>
            {
                object result = null;
                Exception ex = null;
                try
                {
                    result = client.GetServerInfo();
                }
                catch (Exception exc)
                {
                    ex = exc;
                }

                this.Dispatcher.Invoke((Action)delegate
                {
                    this.GetServerInfoButton.IsEnabled = true;
                    if (ex != null)
                    {
                        this.GetServerInfoBox.SelectedObject = ex;
                    }
                    else
                    {
                        this.GetServerInfoBox.SelectedObject = result;
                    }
                });
            }).Start();
        }

        private void GetBalances(object sender, RoutedEventArgs e)
        {
            var account = new Account(this.AccountAddressBox.Text, this.AccountSecretBox.Text);
            this.GetBalancesButton.IsEnabled = false;
            this.GetBalancesBox.SelectedObject = null;

            new Thread(() =>
            {
                object result = null;
                Exception ex = null;
                try
                {
                    result = account.GetBalances(client);
                }
                catch (Exception exc)
                {
                    ex = exc;
                }

                this.Dispatcher.Invoke((Action)delegate
                {
                    this.GetBalancesButton.IsEnabled = true;
                    if (ex != null)
                    {
                        this.GetBalancesBox.SelectedObject = ex;
                    }
                    else
                    {
                        this.GetBalancesBox.SelectedObject = result;
                    }
                });
            }).Start();
        }

        private void GetTrustlines(object sender, RoutedEventArgs e)
        {
            var account = new Account(this.AccountAddressBox.Text, this.AccountSecretBox.Text);
            this.GetTrustlinesButton.IsEnabled = false;
            this.GetTrustlinesBox.SelectedObject = null;

            new Thread(() =>
            {
                object result = null;
                Exception ex = null;
                try
                {
                    result = account.GetTrustlines(client);
                }
                catch (Exception exc)
                {
                    ex = exc;
                }

                this.Dispatcher.Invoke((Action)delegate
                {
                    this.GetTrustlinesButton.IsEnabled = true;
                    if (ex != null)
                    {
                        this.GetTrustlinesBox.SelectedObject = ex;
                    }
                    else
                    {
                        this.GetTrustlinesBox.SelectedObject = result;
                    }
                });
            }).Start();
        }

        private AccountSettings settings;
        private void GetSettings(object sender, RoutedEventArgs e)
        {
            var account = new Account(this.AccountAddressBox.Text, this.AccountSecretBox.Text);
            this.GetSettingsButton.IsEnabled = false;
            this.SetSettingsButton.IsEnabled = false;
            this.GetSettingsBox.SelectedObject = null;

            new Thread(() =>
            {
                object result = null;
                Exception ex = null;
                try
                {
                    result = account.GetSettings(client);
                }
                catch (Exception exc)
                {
                    ex = exc;
                }

                this.Dispatcher.Invoke((Action)delegate
                {
                    this.GetSettingsButton.IsEnabled = true;
                    if (ex != null)
                    {
                        this.SetSettingsButton.IsEnabled = false;
                        this.GetSettingsBox.SelectedObject = ex;
                    }
                    else
                    {
                        this.SetSettingsButton.IsEnabled = true;
                        this.GetSettingsBox.SelectedObject = settings = (AccountSettings) result;
                    }
                });
            }).Start();
        }

        private void AddTrustline(object sender, RoutedEventArgs e)
        {
            var account = new Account(this.AccountAddressBox.Text, this.AccountSecretBox.Text);
            var trustline = new Trustline() { 
                Account = account.Address, 
                Currency = this.AddTrustlineCurrency.Text,
                Limit = this.AddTrustlineLimit.Text, 
                Counterparty = this.AddTrustlineCounterparty.Text
            };
            var allowRippling = this.AddTrustlineAllowRippling.IsChecked ?? false;
            this.AddTrustlineButton.IsEnabled = false;
            this.AddTrustlineBox.SelectedObject = null;

            new Thread(() =>
            {
                object result = null;
                Exception ex = null;
                try
                {
                    result = account.AddTrustline(client, trustline, allowRippling);
                }
                catch (Exception exc)
                {
                    ex = exc;
                }

                this.Dispatcher.Invoke((Action)delegate
                {
                    this.AddTrustlineButton.IsEnabled = true;
                    if (ex != null)
                    {
                        this.AddTrustlineBox.SelectedObject = ex;
                    }
                    else
                    {
                        this.AddTrustlineBox.SelectedObject = result;
                    }
                });
            }).Start();
        }

        private void SetSettings(object sender, RoutedEventArgs e)
        {
            var account = new Account(this.AccountAddressBox.Text, this.AccountSecretBox.Text);
            this.SetSettingsButton.IsEnabled = false;
            this.SetSettingsLabel.Content = "Submiting...";
            this.SetSettingsLabel.Foreground = Brushes.Orange;

            new Thread(() =>
            {
                object result = null;
                Exception ex = null;
                try
                {
                    result = account.SetSettings(client, settings);
                }
                catch (Exception exc)
                {
                    ex = exc;
                }

                this.Dispatcher.Invoke((Action)delegate
                {
                    this.SetSettingsButton.IsEnabled = true;
                    if (ex != null)
                    {
                        this.SetSettingsLabel.Content = ex.ToString();
                        this.SetSettingsLabel.Foreground = Brushes.Red;
                    }
                    else
                    {
                        this.SetSettingsLabel.Content = "Successfully submitted.";
                        this.SetSettingsLabel.Foreground = Brushes.DarkGreen;
                        this.GetSettingsBox.SelectedObject = settings = (AccountSettings)result;
                    }
                });
            }).Start();
        }

        private void FindPaymentPaths(object sender, RoutedEventArgs e)
        {
            var account = new Account(this.AccountAddressBox.Text, this.AccountSecretBox.Text);
            var destAcc = this.FindPaymentPathsAccount.Text;
            var destAmount = this.FindPaymentPathsAmount.Text;
            List<string> src = null;

            if (!string.IsNullOrWhiteSpace(this.FindPaymentPathsSrcCurs.Text))
                src = this.FindPaymentPathsSrcCurs.Text.Split(',').ToList();

            this.FindPaymentPathsButton.IsEnabled = false;
            this.FindPaymentPathsBox.SelectedObject = null;

            new Thread(() =>
            {
                object result = null;
                Exception ex = null;
                try
                {
                    result = account.FindPaymentPaths(client, destAcc, destAmount, src);
                }
                catch (Exception exc)
                {
                    ex = exc;
                }

                this.Dispatcher.Invoke((Action)delegate
                {
                    this.FindPaymentPathsButton.IsEnabled = true;
                    if (ex != null)
                    {
                        this.FindPaymentPathsBox.SelectedObject = ex;
                    }
                    else
                    {
                        this.FindPaymentPathsBox.SelectedObject = result;
                    }
                });
            }).Start();
        }

        private void QueryPayments(object sender, RoutedEventArgs e)
        {
            var account = new Account(this.AccountAddressBox.Text, this.AccountSecretBox.Text);

            this.QueryPaymentsButton.IsEnabled = false;
            this.QueryPaymentsResultBox.SelectedObject = null;

            new Thread(() =>
            {
                object result = null;
                Exception ex = null;
                try
                {
                    result = account.QueryPayments(client, queryOptions);
                }
                catch (Exception exc)
                {
                    ex = exc;
                }

                this.Dispatcher.Invoke((Action)delegate
                {
                    this.QueryPaymentsButton.IsEnabled = true;
                    if (ex != null)
                    {
                        this.QueryPaymentsResultBox.SelectedObject = ex;
                    }
                    else
                    {
                        this.QueryPaymentsResultBox.SelectedObject = result;
                    }
                });
            }).Start();
        }

        private void SubmitPayment(object sender, RoutedEventArgs e)
        {
            var account = new Account(this.AccountAddressBox.Text, this.AccountSecretBox.Text);
            this.SubmitPaymentButton.IsEnabled = false;
            this.SubmitPaymentLabel.Content = "Submiting...";
            this.SubmitPaymentLabel.Foreground = Brushes.Orange;

            new Thread(() =>
            {
                object result = null;
                string key = "";
                Exception ex = null;
                try
                {
                    var pay = account.SubmitPayment(client, payment);
                    result = pay;
                    key = pay.ClientResourceId;
                }
                catch (Exception exc)
                {
                    ex = exc;
                }

                this.Dispatcher.Invoke((Action)delegate
                {
                    this.SubmitPaymentButton.IsEnabled = true;
                    if (ex != null)
                    {
                        this.SubmitPaymentLabel.Content = ex.ToString();
                        this.SubmitPaymentLabel.Foreground = Brushes.Red;
                    }
                    else
                    {
                        this.SubmitPaymentLabel.Content = "Successfully submitted. " + key;
                        this.SubmitPaymentLabel.Foreground = Brushes.DarkGreen;
                        this.SubmitPaymentOptionsBox.SelectedObject = payment = (Payment)result;
                    }
                });
            }).Start();
        }
    }
}
