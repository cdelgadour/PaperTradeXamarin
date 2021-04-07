using MvvmHelpers;
using MvvmHelpers.Commands;
using PaperTradeXamarin.Models;
using PaperTradeXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UltimateXF.Widget.Charts.Models;
using UltimateXF.Widget.Charts.Models.CandleStickChart;
using UltimateXF.Widget.Charts.Models.Formatters;
using UltimateXF.Widget.Charts.Models.LineChart;
using Xamarin.Forms;

namespace PaperTradeXamarin.ViewModels
{
    public class CandleViewModel : BaseViewModel
    {
        public List<Candle> Candles { get; set; }
        public List<string> Labels { get; set; }

        public List<CandleStickEntry> Entries { get; set; }
        public List<EntryChart> LineEntries { get; set; }

        public CandleStickChartData displayData;
        public CandleStickChartData DisplayData { get => displayData; set => SetProperty(ref displayData, value); }

        public LineChartData rsiData;
        
        public LineChartData RSIData { get => rsiData; set => SetProperty(ref rsiData, value); }

        public Xamarin.Forms.Command LoadGraphs { get; set; }

        public string CurrentMarket { get; set; }

        public string charTitle;
        public string CharTitle { get => charTitle; set => SetProperty(ref charTitle, value); }

        public CandleViewModel()
        {
            CurrentMarket = "BTC/USD";
            CharTitle = CurrentMarket;
            Candles = new List<Candle>();
            Entries = new List<CandleStickEntry>();
            LineEntries = new List<EntryChart>();;
            Labels = new List<string>();
            DisplayData = new CandleStickChartData(new List<ICandleStickDataSet>());
            RSIData = new LineChartData(new List<ILineDataSetXF>());
            LoadGraphs = new Xamarin.Forms.Command<string>(LoadMarket);
            InitialData();
        }

        private async void InitialData()
        {
            var candles = await CandleService.GetMarketData(CurrentMarket);
            Candles.AddRange(candles);
            CreateDisplayCandleData();
        }

        async void LoadMarket(string market)
        {
            Entries.Clear();
            Candles.Clear();
            LineEntries.Clear();
            CurrentMarket = market;
            CharTitle = CurrentMarket;
            var candles = await CandleService.GetMarketData(CurrentMarket);
            Candles.AddRange(candles);
            CreateDisplayCandleData();
        }

        private void CreateDisplayCandleData()
        { 
            List<CandleStickEntry> CandleList = new List<CandleStickEntry>();
            List<EntryChart> RsiList = new List<EntryChart>();

            int i = 0;
            foreach (var candle in Candles)
            {
                CandleStickEntry entry = new CandleStickEntry(i, candle.High, candle.Low, candle.Open, candle.Close);
                EntryChart lineEntry = new EntryChart(i, (float)(candle.Open - candle.Close));
                CandleList.Add(entry);
                RsiList.Add(lineEntry);
                i++;
            }
            Entries.AddRange(CandleList);
            LineEntries.AddRange(RsiList);


            var dataSet = new CandleStickDataSet(Entries, "Stocks info")
            {
                DecreasingColor = Color.Red,
                IncreasingColor = Color.Green
            };

            var lineDataSet = new LineDataSetXF(LineEntries, "RSI")
            {
                CircleRadius = 10,
                CircleHoleRadius = 4f,
                CircleColors = new List<Color>(){
                    Color.Accent, Color.Red, Color.Bisque, Color.Gray, Color.Green, Color.Chocolate, Color.Black
                },
                CircleHoleColor = Color.Green,
                ValueColors = new List<Color>() {
                    Color.FromHex("#3696e0"), Color.FromHex("#9958bc"),
                    Color.FromHex("#35ad54"), Color.FromHex("#2d3e52"),
                    Color.FromHex("#e55137"), Color.FromHex("#ea9940"),
                    Color.Black
                },
                Mode = LineDataSetMode.CUBIC_BEZIER
            };

            var dataToDisplay = new CandleStickChartData(new List<ICandleStickDataSet>() { dataSet });
            DisplayData = dataToDisplay;

            var lineDataToDisplay = new LineChartData(new List<ILineDataSetXF>() { lineDataSet });
            RSIData = lineDataToDisplay;
        }
    }
}
