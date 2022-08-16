using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace LabAssignment2
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddViewModel>();
            SimpleIoc.Default.Register<ChangeViewModel>();
            Messenger.Default.Register<NotificationMessage>(this, (message) => { MessageBox.Show(message.Notification); });
        }

        public MainViewModel MainViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public AddViewModel AddViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AddViewModel>(); }
        }

        public ChangeViewModel ChangeViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ChangeViewModel>(); }
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
    }
}