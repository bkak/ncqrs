using System;
using Commands;
using Events;
using Ncqrs.Commanding;
using Ncqrs.NServiceBus;
using NServiceBus;
using ReadModel;
using System.Linq;

namespace Client
{
   public class ClientEndpoint : IWantToRunAtStartup
   {
      public static Guid AggregateId = Guid.NewGuid();

      public IBus Bus { get; set; }

      public void Run1()
      {         
         Console.WriteLine("Press 'Enter' to send a message to create a new Aggregate.To exit, Ctrl + C");
         Console.ReadLine();

         Bus.Send("ServerQueue", new CommandMessage{Payload = new CreateSomeObjectCommand {ObjectId = AggregateId}});            

         string line;
         while ((line = Console.ReadLine()) != null)
         {
            
             ICommand payload = new DoSomethingCommand {Value = line, ObjectId = AggregateId};
            var command = new CommandMessage
                             {
                                Payload = payload
                             };
            Bus.Send("ServerQueue",command);            
         }
      }

      public void Run()
      {
          Console.WriteLine("Press 'P' to create a new PickList.To exit, Ctrl + C");
          Console.WriteLine("Press 'I' to create a Item .To exit, Ctrl + C");
          

          var c = Console.ReadLine();
          
          int pickListNo = new Random().Next();

          Guid pickListId = Guid.Empty;
          if (c != null)
              if (c.Equals("P"))
              {
                  Bus.Send("ServerQueue",
                           new CommandMessage
                               {Payload = new CreatePickList() {Customer = "ABC company", No = pickListNo}});
                  Console.WriteLine("Message successfully published. Press Enter to continue. PickListNo =" + pickListNo);
                  Console.ReadLine();
                  using (var context = new ReadModel.MyNotesReadModelEntities())
                  {
                      var query = from item in context.picklists
                                  where item.Number == pickListNo
                                  select item.PickListId;

                      pickListId = query.Select(x => x).FirstOrDefault();
                  }
                  pickListNo++;
              }

          while ((c = Console.ReadLine()) != null)
          {
              if (c.Equals("I"))
              {
                  Console.WriteLine("Enter Item Name");
                  var itemName = Console.ReadLine();
                  Console.WriteLine("Enter qty");
                  var qty = Console.ReadLine();
                  
                  Bus.Send("ServerQueue", new CommandMessage() {Payload = new AddItem()  {Name =itemName, Qty = Convert.ToInt16(qty)}});
                  Console.WriteLine("Message successfully published. Press Enter to continue");
                  Console.ReadLine();
                  Guid itemCreated;
                  using (var context = new ReadModel.MyNotesReadModelEntities())
                  {
                      var query = from item in context.Items
                                  where item.Name == itemName
                                  select item.Item1;

                      itemCreated = query.Select(x => x).FirstOrDefault();
                  }
                  Console.WriteLine("Item " + itemCreated + " Created.");
                  Console.WriteLine("Press 'A' to add newly created Item to Picklist.To exit, Ctrl + C");
                  c = Console.ReadLine();
                  if ( c!= null && c.Equals("A"))
                  {
                       Console.WriteLine("Enter qty");
                       qty = Console.ReadLine();
                      Bus.Send("ServerQueue",
                               new CommandMessage()
                                   {
                                       Payload =
                                           new AddPickListItem()
                                               {Item = itemCreated, PickListId = pickListId, Price = 3, Qty = Convert.ToInt16( qty)}
                                   });
                      Console.WriteLine("Message successfully published. Press Enter to continue");
                      Console.ReadLine();
                  }
              }
          }
      }
     

       public void Stop()
      {
      }
   }
}
