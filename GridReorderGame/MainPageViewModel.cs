using System;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using Windows.UI.Popups;


namespace GridReorderGame
{
    public class MainPageViewModel
    {
        private DateTime _timeStart,_timeEnd;
        public MainPageViewModel()
        {
            TimeInitialize();
            var _Cards = new ObservableCollection<Card>()
            {
                new Card(){ Name = "1"},
                new Card(){ Name = "2"},
                new Card(){ Name = "3"},
                new Card(){ Name = "4"},
                new Card(){ Name = "5"},
                new Card(){ Name = "6"},
                new Card(){ Name = "7"},
                new Card(){ Name = "8"},
                new Card(){ Name = "9"}
            };
            Cards = new ObservableCollection<Card>(Shuffle(_Cards));
            Cards.CollectionChanged += CardsOnCollectionChanged;
        }

        private void CardsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            int i = 1;
            foreach (var card in Cards)
            {
                if (card.Name != i.ToString())
                {
                    return;
                }
                i++;
            }
            Success();
        }

        private void Success()
        {
            _timeEnd = DateTime.Now;
            var timeTaken = _timeEnd - _timeStart;
            MessageDialog msg = new MessageDialog("Congratulations!!!\nTime taken: " + timeTaken.Minutes + " minutes " + timeTaken.Seconds + " seconds");
            msg.ShowAsync();
        }

        private void TimeInitialize()
        {
            //_timeStart = new DateTime();
            _timeStart = DateTime.Now;
        }


        public ObservableCollection<Card> Cards { get; set; }

        private static IOrderedEnumerable<Card> Shuffle( ObservableCollection<Card> cards)
        {


            IOrderedEnumerable<Card> shuffledCards = cards.OrderBy(a => Guid.NewGuid());
            return shuffledCards;
        }

    }

    public class Card
    {
        public string Name { get; set; }
    }
}
