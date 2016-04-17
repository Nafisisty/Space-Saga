using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Here_History.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Here_History
{
    public partial class HistoryDetail : PhoneApplicationPage
    {
        public HistoryDetail()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var content = PhoneApplicationService.Current.State["param"] as HistoryDataModel;

            titleTextBlock.Text = content.Title;
            descriptionTextBlock.Text = content.Description;
        }
    }
}