using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.UI.Core.ViewModel
{
    [Utilis.RegisterService]
    public class Waitlist : Utilis.UI.ViewModel.Base
    {
        private readonly Service.Waitlist m_svcWaitlist;

        public Waitlist ( )
        {
            // usually service would be injected by the Master controller, but for simplicity sake, we will just fire one up right here.
            m_svcWaitlist = new Service.Waitlist ( new Utility.XmlFilePersister ( System.IO.Path.Combine ( Environment.GetFolderPath ( Environment.SpecialFolder.ApplicationData ), "Homework.Waitlist" ) ) );
            Persons = new Utilis.ObjectModel.ObservableCollection<Service.Data.Person> ( m_svcWaitlist.GetAll ( ) );
            Dequeue = new Utilis.UI.DelegateCommand ( DoDequeue, CanQueueDequeue );
            Enqueue = new Utilis.UI.DelegateCommand ( DoQueue, CanQueueDequeue );

        }

        private void DoQueue ( )
        {
            try
            {
                Service.Data.Person dPerson = m_svcWaitlist.Queue ( Name, Phone );

                Persons.Add ( dPerson );
                Name = null;
                Phone = null;
            }
            catch ( Service.Exception.QueueException qe )
            {
                switch ( qe.ResponseCode )
                {
                    case Service.Exception.QueueException.ResponseCodes.PersonExists:
                        QueueError = "This person already exists.";
                        break;
                    case Service.Exception.QueueException.ResponseCodes.InvalidName:
                        QueueError = "The person name was invalid.";
                        break;
                    case Service.Exception.QueueException.ResponseCodes.InvalidPhone:
                        QueueError = "The person phone number was invalid.";
                        break;
                    default:
                        QueueError = "An unknown error occoured.";
                        break;
                }
            }
        }

        private bool CanQueueDequeue ( )
        {
            return !string.IsNullOrEmpty ( Name ) && !string.IsNullOrEmpty ( Phone );
        }

        private void DoDequeue ( )
        {
            throw new NotImplementedException ( );
            //TODO: impliment this
        }

        private void UpdateCommands ( )
        {
            if ( m_dequeue != null )
                m_dequeue.FireCanExecuteChanged ( );

            if ( m_enqueue != null )
                m_enqueue.FireCanExecuteChanged ( );
        }

        private Utilis.UI.DelegateCommand m_dequeue;
        public Utilis.UI.DelegateCommand Dequeue
        {
            get { return m_dequeue; }
            set { SetProperty ( ref m_dequeue, value ); }
        }

        private Utilis.UI.DelegateCommand m_enqueue;
        public Utilis.UI.DelegateCommand Enqueue
        {
            get { return m_enqueue; }
            set { SetProperty ( ref m_enqueue, value ); }
        }

        private string m_name;
        public string Name
        {
            get { return m_name; }
            set
            {
                if ( SetProperty ( ref m_name, value ) )
                    UpdateCommands ( );
            }
        }

        private string m_phone;
        public string Phone
        {
            get { return m_phone; }
            set
            {
                if ( SetProperty ( ref m_phone, value ) )
                    UpdateCommands ( );
            }
        }

        private string m_queueError;
        public string QueueError
        {
            get { return m_queueError; }
            set { SetProperty ( ref m_queueError, value ); }
        }

        private Utilis.ObjectModel.ObservableCollection<Service.Data.Person> m_persons;
        public Utilis.ObjectModel.ObservableCollection<Service.Data.Person> Persons
        {
            get { return m_persons; }
            set { SetProperty ( ref m_persons, value ); }
        }
    }

    public class WaitlistDesign : Waitlist
    {

    }
}
