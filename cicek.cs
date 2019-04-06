using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BitkiSiniflandırma
{
    class Cicek
    {
        private string cicekTuru;
        private double TacYaprakUzunlugu;
        private double TacYaprakGenisligi;
        private double CanakYaprakUzunlugu;
        private double CanakYaprakGenisligi;
        private double distance;

        public Cicek(string[] arr) {

            this.CanakYaprakUzunlugu1 = double.Parse(arr[0], System.Globalization.CultureInfo.InvariantCulture);
            this.CanakYaprakGenisligi1 = double.Parse(arr[1], System.Globalization.CultureInfo.InvariantCulture);
            this.TacYaprakUzunlugu1 = double.Parse(arr[2], System.Globalization.CultureInfo.InvariantCulture);
            this.TacYaprakGenisligi1 = double.Parse(arr[3], System.Globalization.CultureInfo.InvariantCulture);
            this.cicekTuru = arr[4];

        }
        public Cicek(double[] arr,string turu)
        {

            this.CanakYaprakUzunlugu1 = arr[0];
            this.CanakYaprakGenisligi1 = arr[1];
            this.TacYaprakUzunlugu1 = arr[2];
            this.TacYaprakGenisligi1 = arr[3];
            this.cicekTuru = turu;

        }



        public override string ToString()
        {
            return "Çiçeğin türü " + CicekTuru + "\nCiceğin Canak Yaprak uzunluğu " + this.CanakYaprakUzunlugu + "\n" +
                "Canak Yaprak Genisliği " + this.CanakYaprakGenisligi + "\n" +
                "Tac Yaprak Uzunluğu " + this.TacYaprakUzunlugu + "\nTac Yaprak Genişliği " + this.TacYaprakGenisligi;
                
        }



        public string CicekTuru { get => cicekTuru; set => cicekTuru = value; }
        public double TacYaprakUzunlugu1 { get => TacYaprakUzunlugu; set => TacYaprakUzunlugu = value; }
        public double TacYaprakGenisligi1 { get => TacYaprakGenisligi; set => TacYaprakGenisligi = value; }
        public double CanakYaprakUzunlugu1 { get => CanakYaprakUzunlugu; set => CanakYaprakUzunlugu = value; }
        public double CanakYaprakGenisligi1 { get => CanakYaprakGenisligi; set => CanakYaprakGenisligi = value; }
        public double distance1 { get => distance; set => distance = value; }
    }
}
