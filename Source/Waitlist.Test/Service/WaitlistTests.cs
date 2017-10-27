using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Waitlist.UI.Core.Service.Test
{
    [TestClass]
    public class WaitlistTests
    {
        [TestMethod]
        public void TestEmptyLoad ( )
        {
            Rhino.Mocks.MockRepository oMocks = new Rhino.Mocks.MockRepository ( );

            Utility.IXmlFilePersister oXmlFilePersister = oMocks.StrictMock<Utility.IXmlFilePersister> ( );

            using ( oMocks.Record ( ) )
            {
                Rhino.Mocks.Expect
                    .Call ( oXmlFilePersister.Load ( "" ) )
                    .IgnoreArguments ( )
                    .Return ( null );
            }

            using ( oMocks.Playback ( ) )
            {
                Service.Waitlist svc = new Service.Waitlist ( oXmlFilePersister );
                Service.Data.Person [] aPersons = svc.GetAll ( );

                Assert.IsNotNull ( aPersons );
                Assert.IsTrue ( aPersons.Length == 0 );
            }
        }

        [TestMethod]
        public void TestLoad ( )
        {
            Rhino.Mocks.MockRepository oMocks = new Rhino.Mocks.MockRepository ( );

            Utility.IXmlFilePersister oXmlFilePersister = oMocks.StrictMock<Utility.IXmlFilePersister> ( );

            using ( oMocks.Record ( ) )
            {
                Rhino.Mocks.Expect
                    .Call ( oXmlFilePersister.Load ( "" ) )
                    .IgnoreArguments ( )
                    .Return ( System.Xml.Linq.XElement.Parse ( "<Waitlist><Persons><Person Id=\"47a73298-d91c-4c94-80aa-d935c9b0873e\"><Name>50ffab1f-b894-4632-898d-2c6588856641</Name><Phone>5be558b7-2c57-4156-96e0-d2442bc41975</Phone></Person></Persons></Waitlist>" ) );
            }

            using ( oMocks.Playback ( ) )
            {
                Service.Waitlist svc = new Service.Waitlist ( oXmlFilePersister );
                Data.Person [] aPersons = svc.GetAll ( );

                Assert.IsNotNull ( aPersons );
                Assert.AreEqual ( 1, aPersons.Length );

                Assert.AreEqual ( new Guid ( "47a73298-d91c-4c94-80aa-d935c9b0873e" ), aPersons [ 0 ].Id );
                Assert.AreEqual ( "50ffab1f-b894-4632-898d-2c6588856641", aPersons [ 0 ].Name );
                Assert.AreEqual ( "5be558b7-2c57-4156-96e0-d2442bc41975", aPersons [ 0 ].Phone );
            }
        }

        [TestMethod]
        public void TestQueueWithEmptyList ( )
        {
            Rhino.Mocks.MockRepository oMocks = new Rhino.Mocks.MockRepository ( );

            Utility.IXmlFilePersister oXmlFilePersister = oMocks.StrictMock<Utility.IXmlFilePersister> ( );

            using ( oMocks.Record ( ) )
            {
                Rhino.Mocks.Expect
                    .Call ( oXmlFilePersister.Load ( "" ) )
                    .IgnoreArguments ( )
                    .Return ( null );

                Rhino.Mocks.Expect
                    .Call ( ( ) => oXmlFilePersister.Save ( "", null ) )
                    .IgnoreArguments ( );
            }

            using ( oMocks.Playback ( ) )
            {
                Service.Waitlist svc = new Service.Waitlist ( oXmlFilePersister );

                string sName = Guid.NewGuid ( ).ToString ( );
                string sPhone = Guid.NewGuid ( ).ToString ( );

                Data.Person dPerson = svc.Queue ( sName, sPhone );

                Assert.IsNotNull ( dPerson );
                Assert.AreEqual ( sName, dPerson.Name );
                Assert.AreEqual ( sPhone, dPerson.Phone );
                Assert.AreNotEqual ( Guid.Empty, dPerson.Id );
            }
        }

        [TestMethod]
        [ExpectedException ( typeof ( Service.Exception.DequeueException ) )]
        public void TestDequeueWithEmptyList ( )
        {
            Rhino.Mocks.MockRepository oMocks = new Rhino.Mocks.MockRepository ( );

            Utility.IXmlFilePersister oXmlFilePersister = oMocks.StrictMock<Utility.IXmlFilePersister> ( );

            using ( oMocks.Record ( ) )
            {
                Rhino.Mocks.Expect
                    .Call ( oXmlFilePersister.Load ( "" ) )
                    .IgnoreArguments ( )
                    .Return ( null );

                Rhino.Mocks.Expect
                    .Call ( ( ) => oXmlFilePersister.Save ( "", null ) )
                    .IgnoreArguments ( );
            }

            using ( oMocks.Playback ( ) )
            {
                Service.Waitlist svc = new Service.Waitlist ( oXmlFilePersister );

                svc.Dequeue ( new Data.Person ( ) { Id = Guid.NewGuid ( ), Name = Guid.NewGuid ( ).ToString ( ), Phone = Guid.NewGuid ( ).ToString ( ) } );
            }
        }

        [TestMethod]
        [ExpectedException ( typeof ( Service.Exception.DequeueException ) )]
        public void TestDequeueNotFound ( )
        {
            Rhino.Mocks.MockRepository oMocks = new Rhino.Mocks.MockRepository ( );

            Utility.IXmlFilePersister oXmlFilePersister = oMocks.StrictMock<Utility.IXmlFilePersister> ( );

            using ( oMocks.Record ( ) )
            {
                Rhino.Mocks.Expect
                    .Call ( oXmlFilePersister.Load ( "" ) )
                    .IgnoreArguments ( )
                    .Return ( System.Xml.Linq.XElement.Parse ( "<Waitlist><Persons><Person Id=\"47a73298-d91c-4c94-80aa-d935c9b0873e\"><Name>50ffab1f-b894-4632-898d-2c6588856641</Name><Phone>5be558b7-2c57-4156-96e0-d2442bc41975</Phone></Person></Persons></Waitlist>" ) );

                Rhino.Mocks.Expect
                    .Call ( ( ) => oXmlFilePersister.Save ( "", null ) )
                    .IgnoreArguments ( );
            }

            using ( oMocks.Playback ( ) )
            {
                Service.Waitlist svc = new Service.Waitlist ( oXmlFilePersister );

                svc.Dequeue ( new Data.Person ( ) { Id = Guid.NewGuid ( ), Name = Guid.NewGuid ( ).ToString ( ), Phone = Guid.NewGuid ( ).ToString ( ) } );
            }
        }

        [TestMethod]
        public void TestDequeue ( )
        {
            Rhino.Mocks.MockRepository oMocks = new Rhino.Mocks.MockRepository ( );

            Utility.IXmlFilePersister oXmlFilePersister = oMocks.StrictMock<Utility.IXmlFilePersister> ( );

            using ( oMocks.Record ( ) )
            {
                Rhino.Mocks.Expect
                    .Call ( oXmlFilePersister.Load ( "" ) )
                    .IgnoreArguments ( )
                    .Return ( System.Xml.Linq.XElement.Parse ( "<Waitlist><Persons><Person Id=\"47a73298-d91c-4c94-80aa-d935c9b0873e\"><Name>50ffab1f-b894-4632-898d-2c6588856641</Name><Phone>5be558b7-2c57-4156-96e0-d2442bc41975</Phone></Person></Persons></Waitlist>" ) );

                Rhino.Mocks.Expect
                    .Call ( ( ) => oXmlFilePersister.Save ( "", null ) )
                    .IgnoreArguments ( );
            }

            using ( oMocks.Playback ( ) )
            {
                Service.Waitlist svc = new Service.Waitlist ( oXmlFilePersister );

                svc.Dequeue ( new Data.Person ( ) { Id = new Guid ( "47a73298-d91c-4c94-80aa-d935c9b0873e" ), Name = "50ffab1f-b894-4632-898d-2c6588856641", Phone = "5be558b7-2c57-4156-96e0-d2442bc41975" } );
            }
        }
    }
}
