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
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Member> members = new ObservableCollection<Member>();

        public ObservableCollection<Member> membersList
        {
            get
            {
                return members;
            }
        }

        private Member newMember = new Member();
        private Member selectedMember;
        private MemberDB database;
        private AddWindow _addWindow;

        public Member NewMember
        {
            get
            {
                return newMember;
            }
            set
            {
                newMember = value;
                RaisePropertyChanged("NewMember");
            }
        }
        
        public MainViewModel()
        {
            //members = 
            database = new MemberDB(members);
            members = database.GetMemberships();
            AddCommand = new RelayCommand(AddMethod);
            ExitCommand = new RelayCommand<IClosable>(ExitMethod);
            ChangeCommand = new RelayCommand(ChangeMethod);
            SaveCommand = new RelayCommand(SaveMethod);
            CancelCommand = new RelayCommand(CancelMethod);

            Messenger.Default.Register<MessageMember>(this, ReceiveMember);
            Messenger.Default.Register<NotificationMessage>(this, ReceiveMessage);
        }
        
        public ICommand AddCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public ICommand ChangeCommand { get; private set; }


        private void AddMethod()
        {
            _addWindow = new AddWindow();
            _addWindow.Show();
        }

        private void CancelMethod()
        {
            _addWindow.Close();
        }

        private void LoadEmployees()
        {
            members = Member.GetExampleMembers();
            RaisePropertyChanged(() => membersList);
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Members loaded"));
        }

        private void SaveMethod()
        {
            ReceiveMember(new MessageMember(newMember.FirstName, newMember.LastName, newMember.Email, "Added"));
        }
        
        public void ExitMethod(IClosable window)
        {
            if (window != null) { 
            window.Close();
            }
        }

        public void ChangeMethod()
        {
            if (SelectedMember != null)
            {
                ChangeWindow change = new ChangeWindow();
                change.Show();
                Messenger.Default.Send(selectedMember);
            }
        }

        public Member SelectedMember
        {
            get
            {
                return selectedMember;
            }
            set
            {
                selectedMember = value;
                RaisePropertyChanged("SelectedMember");
            }
        }

        public void ReceiveMember(MessageMember m)
        {
            if (m.Message == "Updated")
            {
                int index = MemberList.IndexOf(selectedMember);
                MemberList.Insert(index, new Member { FirstName = m.FirstName, LastName = m.LastName, Email = m.Email });
                MemberList.RemoveAt(index + 1);
                database.SaveMemberships();
            }
            
            else if (m.Message == "Added")
            {
                MemberList.Add(new Member { FirstName = m.FirstName, LastName = m.LastName, Email = m.Email });
                database.SaveMemberships();
            }
        }

        public void ReceiveMessage(NotificationMessage msg)
        {
            if (msg.Notification == "Deleted")
            {
                MemberList.Remove(selectedMember);
                database.SaveMemberships();
            }
        }

        public ObservableCollection<Member> MemberList
        {
            get { return members; }
        }
    }
}