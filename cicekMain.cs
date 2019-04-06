using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;




namespace BitkiSiniflandırma
{
    class Program
    {
        static void Main(string[] args)
        {
            int cikis = 1;
            string[] text = System.IO.File.ReadAllLines("Veriseti.txt");
            List<Cicek> CicekListesi = CicekListesiOlustur(text);//Bütün çiçekleri barındıracak olan ArrayListimiz
            Boolean devamEt;
            int k; // komşu sayımız
            ArrayList uzakliklar = new ArrayList(); // Bütün uzaklıkları bir ArrayListte tutup bu listeyi sıralayıp en küçük k tane uzaklığı çekeceğimiz arraylist.
            Double[] girilenCicek = new Double[4];//Elle girilecek olan çiçeğin değerlerini tutacağımız dizi
            while (cikis != 0)
            {
                Console.WriteLine("Menü " +
                   "\n 1-) Elle veri girerek bitkiyi tahmin ettirmek istiyorum. (Elle girdiğiniz veriyi ekleme opsiyonu da sunar.)" +
                   "\n 2-) Kontrol veri setinin kullanılarak programın başarısının ölçülmesini istiyorum" +
                   "\n 3-) silme işlemleri yapmak istiyorum" +
                   "\n 4-) Listeleme yapmak istiyorum");
                Console.WriteLine("Yapmak istediğiniz işlemin kodunu giriniz. Elle veri girmek için '1' gibi");
                int menu = Convert.ToInt32(Console.ReadLine());
                if (menu == 1)
                {

                    
                        girilenCicekDizisiniDuzenle(girilenCicek);/* daha temiz bir kod olması açısından girilen çiçeğin 
                bilgilerinin işlenebilir duruma gelmesini bu method içerisinde sağladık*/
                        Console.WriteLine("Kaç adet çiçek arasında komşuluk aranmasını istersiniz?(k)");
                        k = Convert.ToInt32(Console.ReadLine());
                        uzaklikBul(CicekListesi, uzakliklar, girilenCicek);
                        uzakliklar.Sort();
                        Cicek[] komsuCicekler = komsuCicekleriBul(uzakliklar, CicekListesi, k);
                        komsulariYazdir(komsuCicekler);
                        string ciceginTuru = ciceginTurunuBelirle(komsuCicekler);
                        Console.WriteLine(" ");
                        Console.WriteLine("Klavyeden girilen çiçeği türü {0}", ciceginTuru);
                        Console.WriteLine("Girdiğiniz çiçeği veri setine eklemek için 1'i eklemeden geçmek için herhangi bir sayıyı tuşlayınız.");
                        int ekleme = Convert.ToInt32(Console.ReadLine());
                        if (ekleme == 1)
                        {
                            Cicek cicek = new Cicek(girilenCicek, ciceginTuru);
                            CicekListesi.Add(cicek);
                        }

                        /*
                        //kullanıcının dilediği kadar çiçek girebilmesi için yazılmış basit bir komut dizisi
                        Console.WriteLine("Yeni bir çiçek girmek istiyorsanız 1'i, çıkış için herhangi bir sayıyı tuşlayınız.");
                        int secim = Convert.ToInt32(Console.ReadLine());
                        if (secim == 1) devamEt = true;
                        else devamEt = false;
                        
                    } while (devamEt == true);*/

                }
                else if (menu == 2)
                {
                    int dogruTahmin = 0;
                    string[] metin = System.IO.File.ReadAllLines(@"Test Veriseti.txt");
                    List<Cicek> TestCicekVeriseti = CicekListesiOlustur(metin);//Test için veriseti oluşturacak çiçekleri barındıracak olan Generic Listimiz
                    metin = System.IO.File.ReadAllLines(@"Test.txt");
                    List<Cicek> TestEdilecekCicekler = CicekListesiOlustur(metin);//Test edilecek çiçekleri barındıracak olan Generic Listimiz
                    ArrayList TestEdilecekCiceklerinBilgileri = girilenCicekDizisiniDuzenle(TestEdilecekCicekler);
                    Console.WriteLine("Kaç adet çiçek arasında komşuluk aranmasını istersiniz?(k)");
                    k = Convert.ToInt32(Console.ReadLine());



                    int n = 0;
                    foreach (double[] cicekBilgileri in TestEdilecekCiceklerinBilgileri)
                    {
                        uzakliklar = new ArrayList();
                        uzaklikBul(TestCicekVeriseti, uzakliklar, cicekBilgileri);
                        uzakliklar.Sort();
                        Cicek[] komsuCicekler = komsuCicekleriBul(uzakliklar, TestCicekVeriseti, k);
                        Console.WriteLine("####################################################################################");
                        Console.WriteLine("{0}. verinin komşuları:", (n + 1));
                        komsulariYazdir(komsuCicekler);
                        string ciceginTuru = ciceginTurunuBelirle(komsuCicekler);
                        Cicek siradakiCicek = TestEdilecekCicekler[n];
                        Console.WriteLine(" ");
                        Console.WriteLine("{0}. çiçeğin türü {1} , tahmin edilen {2}", (n + 1), siradakiCicek.CicekTuru, ciceginTuru);
                        Console.WriteLine("####################################################################################");
                        if (siradakiCicek.CicekTuru.Equals(ciceginTuru))
                        {

                            dogruTahmin++;
                        }
                        n++;
                    }
                    double tahminPuani = Convert.ToDouble((dogruTahmin * 100) / TestEdilecekCicekler.Count);
                    Console.WriteLine("Programın Başarılı tahmin yüzdesi = %{0}", tahminPuani);
                }


                else if (menu == 3)
                {
                    Console.WriteLine("İndisini girerek silmek için 1'i , bütün verileri silmek için 0'ı tuşlayınız.");
                    int silme = Convert.ToInt32(Console.ReadLine());
                    if (silme == 0)
                    {
                        CicekListesi = new List<Cicek>();
                    }
                    else if (silme == 1)
                    {
                        Console.WriteLine("Silmek istediğiniz verinin indisini giriniz");
                        silme = Convert.ToInt32(Console.ReadLine());
                        CicekListesi.RemoveAt(silme);
                    }
                    else Console.WriteLine("Hatalı bir değer girdiniz.");

                }


                else if (menu == 4)
                {
                    CicekListesiYazdir(CicekListesi);
                }

                Console.WriteLine("Çıkış için 0'ı menüye dönmek için herhangi bir sayıyı tuşlayabilirsiniz.");
                cikis = Convert.ToInt32(Console.ReadLine());

            }
            
            Console.WriteLine("Çıkış için herhangi bir tuşa basabilirsiniz.");
            Console.ReadKey();
        }


        

        

        public static List<Cicek> CicekListesiOlustur(string[] text)
        {

            List<Cicek> arr = new List<Cicek>();

            foreach (string word in text)
            {

                string[] charArray = word.Split(',');
                Cicek cicek = new Cicek(charArray);
                arr.Add(cicek);

            }
            return arr;
        }

        public static void CicekListesiYazdir(List<Cicek> arr)
        {

            foreach (Cicek cicek in arr)
            {
                
                Console.WriteLine(cicek);
                Console.WriteLine("##########################################");
            }
        }
        public static void girilenCicekDizisiniDuzenle(double[] arr)
        {

            Console.WriteLine(" Çiçeğiniz çanak yaprak uzunluğu nedir?");
            double canakU = double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
            arr[0] = canakU;
            Console.WriteLine("Çiçeğinizin çanak yaprak genişliği nedir?");
            double canakG = double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
            arr[1] = canakG;
            Console.WriteLine("Çiçeğinizin taç yaprak uzunluğu nedir?");
            double tacU = double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
            arr[2] = tacU;
            Console.WriteLine("Çiçeğinizin taç yaprak genişliği nedir?");
            double tacG = double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
            arr[3] = tacG;

        }
        public static ArrayList girilenCicekDizisiniDuzenle(List<Cicek> TestCicekListesi)
        {
            ArrayList cicekBilgileri = new ArrayList();
            
            foreach (Cicek cicek in TestCicekListesi) {
                double[] arr = new double[4];
                arr[0] = cicek.CanakYaprakUzunlugu1;
                arr[1] = cicek.CanakYaprakGenisligi1;
                arr[2] = cicek.TacYaprakUzunlugu1;
                arr[3] = cicek.TacYaprakGenisligi1;
                cicekBilgileri.Add(arr);
            }
            return cicekBilgileri;

        }

        public static void uzaklikBul(List<Cicek> cicekListesi, ArrayList uzakliklar, double[] girilenCicek)
        {


            double d = 0;
            foreach (Cicek cicek in cicekListesi)
            {

                double[] cicekArr = cicegiArrayYap(cicek);
                for (int i = 0; i < 4; i++)
                {


                    d += Convert.ToDouble(Math.Pow(cicekArr[i] - girilenCicek[i], 2));

                }

                d = Convert.ToDouble(Math.Sqrt(d));
                uzakliklar.Add(d);
                cicek.distance1 = d;
            }

        }
        public static double[] cicegiArrayYap(Cicek cicek)
        {

            double[] cicekArr = new double[4];
            cicekArr[0] = cicek.CanakYaprakUzunlugu1;
            cicekArr[1] = cicek.CanakYaprakGenisligi1;
            cicekArr[2] = cicek.TacYaprakUzunlugu1;
            cicekArr[3] = cicek.TacYaprakGenisligi1;
            return cicekArr;
        }
        public static Cicek[] komsuCicekleriBul(ArrayList uzakliklar, List<Cicek> cicekListesi, int k)
        {

            int j = 0;
            Cicek[] komsuCicekler = new Cicek[k];
            double[] enYakinUzakliklar = new double[k];
            for (int i = 0; i < k; i++)
            {
                enYakinUzakliklar[i] = Convert.ToDouble(uzakliklar[i]);// en yakın değerleri bir listede tutuyoruz.
            }
            foreach (double d in enYakinUzakliklar)
            {
                foreach (Cicek cicek in cicekListesi)
                {

                    if (cicek.distance1 == d)
                    {
                        if (j == k) return komsuCicekler;
                        komsuCicekler[j] = cicek;
                        j++;
                    }

                }
            }

            return komsuCicekler;
        }

        public static void ciceklereYakinlikEkle(List<Cicek> cicekListesi, double[] uzakliklar)
        {

            for (int i = 0; i < cicekListesi.Capacity; i++)
            {

                cicekListesi[i].distance1 = uzakliklar[i];

            }

        }

        public static string ciceginTurunuBelirle(Cicek[] komsuCicekler)
        {
            string cicekTuru = " ";
            int[] puanlandırma = { 0, 0, 0 };

            foreach (Cicek cicek in komsuCicekler) {
                if (cicek == null) {
                    Console.WriteLine("Girdiğiniz çiçeğin bir zambak olması mümkün değil. Lütfen ölçümlerinizi kontrol ediniz");
                    return " ";
                }
            }

            foreach (Cicek cicek in komsuCicekler)
            {
                    
                
                if (cicek.CicekTuru.Equals("Iris-setosa", StringComparison.Ordinal))
                {
                    puanlandırma[0]++;
                }
                else if (cicek.CicekTuru.Equals("Iris-versicolor", StringComparison.Ordinal))
                {
                    puanlandırma[1]++;
                }
                else if (cicek.CicekTuru.Equals("Iris-virginica", StringComparison.Ordinal))
                {
                    puanlandırma[2]++;
                }

                
                }
            if (puanlandırma[0] == puanlandırma[1] || puanlandırma[0] == puanlandırma[2] || puanlandırma[2] == puanlandırma[1])
            {
                cicekTuru = komsuCicekler[0].CicekTuru;
            }
            else
            {
                int max = puanlandırma.Max();//methodu tekrar tekrar çağırıp işlemciyi yormamak adına Max değişkenini kullanıyoruz.
                if (puanlandırma[0] == max)
                {
                    cicekTuru = "Iris-setosa";

                }
                else if (puanlandırma[1] == max)
                {
                    cicekTuru = "Iris-versicolor";

                }
                else if (puanlandırma[2] == max)
                {
                    cicekTuru = "Iris-virginica";

                }

            }
        return cicekTuru;

            }

        public static void komsulariYazdir(Cicek[] komsuCicekler) {

            foreach (Cicek cicek in komsuCicekler)
            {
                Console.WriteLine(cicek);
                Console.WriteLine(" ");
            }

        }
            
        }
    }
