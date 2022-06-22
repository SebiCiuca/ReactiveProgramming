using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleOne
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var searchStream = Observable.FromEventPattern(
                //subscribe on text change
                 s => searchTextBox.TextChanged += s,
                 s => searchTextBox.TextChanged -= s)
                //select text
                 .Select(evt => searchTextBox.Text) // better to select on the UI thread
                 //wait for 0.5 seconds before triggering search
                 .Throttle(TimeSpan.FromMilliseconds(500))
                 //don't trigger if user didn't click anything
                 .DistinctUntilChanged()
                 // placement of this is important to avoid races updating the UI
                 .ObserveOn(SynchronizationContext.Current)
                 .Do(_ =>
                 {
                     //remove old results
                     resultListBox.Items.Clear();
                 })
                 .SelectMany(
                    //search in hole word list
                    text => AsyncLookupInList(text));


            searchStream.Subscribe(
                    results =>
                        //when search is over add items to list box
                        resultListBox.Items.AddRange(results.ToArray()),
                    error => Console.WriteLine(error));
        }


        private IObservable<string[]> AsyncLookupInList(string text)
        {
            var matchingWords = words.Where(w => w.StartsWith(text)).ToArray();

            return Observable.Return(matchingWords);
        }

        private List<string> words = new List<string>
        {
            "Banana",
            "Apple",
            "Barman",
            "Aplication",
            "Caramel",
            "Car"
        };

    }
}
