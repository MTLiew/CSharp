using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LabAssignment2;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
namespace LabAssignment2
{
    public class ChangeViewModel : ViewModelBase
    {
        private string enteredFName;
        private string enteredLName;
        private string enteredEmail;

        public ChangeViewModel()
        {
            Messenger.Default.Register<Member>(this, GetSelected);
            UpdateCommand = new RelayCommand<IClosable>(UpdateMethod);
            DeleteCommand = new RelayCommand<IClosable>(DeleteMethod);
        }

        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
    
        public void UpdateMethod(IClosable window)
        {
            try
            {
                Messenger.Default.Send<MessageMember>(new MessageMember(EnteredFName, EnteredLName, EnteredEmail, "Updated"));
                window.Close();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Fields must be under 25 characters.", "Entry Error");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Fields cannot be empty.", "Entry Error");
            }
            catch (FormatException)
            {
                MessageBox.Show("Must be a valid e-mail address.", "Entry Error");
            }
        }

        public void DeleteMethod(IClosable window)
        {
            if (window != null)
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Deleted"));
                window.Close();
            }
        }

        public void GetSelected(Member m)
        {
            EnteredFName = m.FirstName;
            EnteredLName = m.LastName;
            EnteredEmail = m.Email;
        }
       
        public string EnteredFName
        {
            get
            {
                return enteredFName;
            }
            set
            {
                enteredFName = value;
                RaisePropertyChanged("EnteredFName");
            }
        }

        public string EnteredLName
        {
            get
            {
                return enteredLName;
            }
            set
            {
                enteredLName = value;
                RaisePropertyChanged("EnteredLName");
            }
        }

        public string EnteredEmail
        {
            get
            {
                return enteredEmail;
            }
            set
            {
                enteredEmail = value;
                RaisePropertyChanged("EnteredEmail");
            }
        }
    }
}