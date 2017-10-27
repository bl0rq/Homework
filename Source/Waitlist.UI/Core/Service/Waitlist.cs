using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilis.Extensions;

namespace Waitlist.UI.Core.Service
{
    public class Waitlist
    {
        private static readonly object ms_oSync = new object ( );

        private readonly Utility.IXmlFilePersister m_oXmlFilePersister;

        private readonly Adapter.PersonFromXml m_adpPersonFromXml = new Adapter.PersonFromXml ( );
        private readonly Adapter.PersonToXml m_adpPersonToXml = new Adapter.PersonToXml ( );
        internal const string mc_sStorageName = "WaitList";

        public Waitlist ( Utility.IXmlFilePersister oXmlFilePersister )
        {
            m_oXmlFilePersister = oXmlFilePersister;
        }

        public Data.Person [] GetAll ( )
        {
            System.Xml.Linq.XElement xWaitList;
            lock ( ms_oSync )
            {
                xWaitList = m_oXmlFilePersister.Load ( mc_sStorageName );
            }

            if ( xWaitList == null || xWaitList.Element ( "Persons" ) == null )
                return new Data.Person [] { };
            else
                return xWaitList
                    .Element ( "Persons" )
                    .Elements ( "Person" )
                    .Select ( xPerson => m_adpPersonFromXml.Adapt ( xPerson ) ).ToArray ( );
        }

        public Data.Person Queue ( string sName, string sPhone )
        {
            Data.Person dPerson = new Data.Person ( )
            {
                Id = Guid.NewGuid ( ),
                Name = sName,
                Phone = sPhone
            };

            // normally this logic would exist on the server side.
            if ( string.IsNullOrEmpty ( sName ) )
                throw new Exception.QueueException ( Exception.QueueException.ResponseCodes.InvalidName );
            else if ( string.IsNullOrEmpty ( sPhone ) )
                throw new Exception.QueueException ( Exception.QueueException.ResponseCodes.InvalidPhone );

            lock ( ms_oSync )
            {
                System.Xml.Linq.XElement xWaitList = m_oXmlFilePersister.Load ( mc_sStorageName ) ?? new System.Xml.Linq.XElement ( "Waitlist" );

                System.Xml.Linq.XElement xPersons = GetPersons ( xWaitList );

                if ( xPersons.Elements ( "Person" )
                        .Select ( xPerson => m_adpPersonFromXml.Adapt ( xPerson ) )
                        .Any ( dPersonSearch => dPersonSearch == dPerson ) )
                    throw new Exception.QueueException ( Exception.QueueException.ResponseCodes.PersonExists );

                xPersons.Add ( m_adpPersonToXml.Adapt ( dPerson ) );

                m_oXmlFilePersister.Save ( mc_sStorageName, xWaitList );
            }

            return dPerson;
        }

        public void Dequeue ( Data.Person dPerson )
        {
            if ( dPerson == null )
                throw new Exception.DequeueException ( Exception.DequeueException.ResponseCodes.InvalidPerson );

            lock ( ms_oSync )
            {
                System.Xml.Linq.XElement xWaitList = m_oXmlFilePersister.Load ( mc_sStorageName ) ?? new System.Xml.Linq.XElement ( "Waitlist" );

                System.Xml.Linq.XElement xPersons = GetPersons ( xWaitList );

                System.Xml.Linq.XElement xPerson = xPersons
                    .Elements ( "Person" )
                    .Where ( xPersonSearch => xPersonSearch.Attribute ( "Id" ).GetValue ( Guid.Empty, s => new Guid ( s ) ) == dPerson.Id )
                    .FirstOrDefault ( );

                if ( xPerson == null )
                    throw new Exception.DequeueException ( Exception.DequeueException.ResponseCodes.PersonNotFound );
                else
                {
                    xPerson.Remove ( );
                    m_oXmlFilePersister.Save ( mc_sStorageName, xWaitList );
                }
            }
        }

        private System.Xml.Linq.XElement GetPersons ( System.Xml.Linq.XElement xWaitList )
        {
            System.Xml.Linq.XElement xPersons = xWaitList.Element ( "Persons" );

            if ( xPersons == null )
            {
                xPersons = new System.Xml.Linq.XElement ( "Persons" );
                xWaitList.Add ( xPersons );
            }

            return xPersons;
        }
    }
}
