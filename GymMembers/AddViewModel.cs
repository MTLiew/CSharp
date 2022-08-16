using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
namespace LabAssignment2
{
    public class AddViewModel : ViewModelBase
    {
        private string enteredFName;
        private string enteredLName;
        private string enteredEmail;

        public AddViewModel()
        {
            SaveCommand = new RelayCommand<IClosable>(SaveMethod);
            CancelCommand = new RelayCommand<IClosable>(CancelMethod);
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        
        /// <summary>
        /// Sends a valid member to the Main VM to add to the list, then closes the 
        /// window.
        /// </summary>
        /// <param name="window">The window to close.</param>
        
        public void SaveMethod(IClosable window)
        {
            try
            {
                Messenger.Default.Send<MessageMember>(new MessageMember(EnteredFName, EnteredLName, EnteredEmail, "Added"));
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
        
        public void CancelMethod(IClosable window)
        {
            window.Close();
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