using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SongOnIsland : MonoBehaviour
{
    public static SongOnIsland instance;

    // Define a structure to hold song details
    [System.Serializable]
    public class Song
    {
        public string region; // Region identifier (e.g., "sumatera", "jawa", etc.)
        public string name;
        public string details;
        public List<string> midiFilePaths;

        public Song()
        {
            midiFilePaths = new List<string>();
        }
    }

    // List to hold all songs
    public List<Song> songs;

    void Awake()
    {
        instance = this;
    }

    public void OnButtonClick(string region)
    {
        // Clear previous list if needed
        ClearList();

        // Add songs based on the button clicked
        if (region == "sumatera")
        {
            Song soleram = new Song
            {
                region = "sumatera",
                name = "Soleram",
                details = "Soleram atau Suliram (disebut juga Soreram atau Suriram) adalah lagu daerah yang berasal dari Provinsi Riau, Indonesia. Isi lagunya terutama menceritakan tentang cinta dan persahabatan. Lirik lagu Soleram cukup pendek, mendayu-dayu, merdu, dan mudah diingat."
            };
            soleram.midiFilePaths.Add("Soleram_1.mid");
            soleram.midiFilePaths.Add("Soleram_2.mid");
            soleram.midiFilePaths.Add("Soleram_3.mid");
            songs.Add(soleram);

            Song lancangKuning = new Song
            {
                region = "sumatera",
                name = "Lancang Kuning",
                details = "Lagu Lancang Kuning merupakan sebagai pedoman bagi masyarakat yang berisikan ajaran seorang pemimpin guna menjalankan tugasnya di tengah-tengah masyarakat. Lirik lagu ini menggambarkan bagaimana seorang nakhoda yang berlayar dengan mengemudi kapalnya"
            };
            lancangKuning.midiFilePaths.Add("Lancang_Kuning_1.mid");
            lancangKuning.midiFilePaths.Add("Lancang_Kuning_2.mid");
            lancangKuning.midiFilePaths.Add("Lancang_Kuning_3.mid");
            songs.Add(lancangKuning);

            Song sinanggartulo = new Song
            {
                region = "sumatera",
                name = "Sinanggar Tulo",
                details = "\"Sinanggar Tulo\" menceritakan keluh kesar seorang pemuda yang harus menuruti perintang sang ibu. Sang ibu meminta anaknya mencari wanita dari marga Tobing. Marga tersebut merupakan marga yang sama dengan ibunya."
            };
            sinanggartulo.midiFilePaths.Add("Sinanggar_Tulo_1.mid");
            sinanggartulo.midiFilePaths.Add("Sinanggar_Tulo_2.mid");
            sinanggartulo.midiFilePaths.Add("Sinanggar_Tulo_3.mid");
            songs.Add(sinanggartulo);

            // Add more songs to sumatera region as needed
        }
        else if (region == "jawa")
        {
            Song haloBandung = new Song
            {
                region = "jawa",
                name = "Halo-Halo Bandung",
                details = "Halo, Halo Bandung adalah satu lagu perjuangan Indonesia ciptaan Ismail Marzuki yang menggambarkan semangat perjuangan rakyat kota Bandung dalam masa pascakemerdekaan pada tahun 1946, khususnya dalam peristiwa Bandung Lautan Api yang terjadi pada tanggal 23 Maret 1946."
            };
            haloBandung.midiFilePaths.Add("Halo-Halo_Bandung_1.mid");
            haloBandung.midiFilePaths.Add("Halo-Halo_Bandung_2.mid");
            haloBandung.midiFilePaths.Add("Halo-Halo_Bandung_3.mid");
            songs.Add(haloBandung);

            Song tokecang = new Song
            {
                region = "jawa",
                name = "Tokecang",
                details = "Lagu \"Tokecang\" merupakan singkatan dari tokek makan kacang. Lagu ini memiliki makna tentang seseorang yang makan terlalu banyak dan rakus hingga semua makanannya habis dan dibagi dengan orang lain."
            };
            tokecang.midiFilePaths.Add("Tokecang_1.mid");
            tokecang.midiFilePaths.Add("Tokecang_2.mid");
            tokecang.midiFilePaths.Add("Tokecang_3.mid");
            songs.Add(tokecang);

            Song jali = new Song
            {
                region = "jawa",
                name = "Jali-Jali",
                details = "\"Jali-jali\" atau \"Si Jali-jali\" adalah lagu daerah Betawi dari Jakarta, Indonesia. Lagu ini berasal dari kata buah Jali. Lagu jali-jali yang merupakan salah satu khasanah musik dan lagu yang berasal dari Betawi ini, asal usulnya diyakini lahir, dikembangkan oleh kaum China peranakan Jakarta."
            };
            jali.midiFilePaths.Add("Jali-Jali_1.mid");
            jali.midiFilePaths.Add("Jali-Jali_2.mid");
            jali.midiFilePaths.Add("Jali-Jali_3.mid");
            songs.Add(jali);

            // Add more songs to jawa region as needed
        }
        else if (region == "kalimantan")
        {
            Song amparPisang = new Song
            {
                region = "kalimantan",
                name = "Ampar-Ampar Pisang",
                details = "Ampar-ampar Pisang adalah lagu daerah Indonesia berbahasa Banjar, ciptaan Hamiedan AC. Lagu ini bercerita tentang pisang yang diolah dengan cara diamparkan/dijemur dalam proses pengolahan pisang menjadi makanan khas."
            };
            amparPisang.midiFilePaths.Add("Ampar-Ampar_Pisang_1.mid");
            amparPisang.midiFilePaths.Add("Ampar-Ampar_Pisang_2.mid");
            amparPisang.midiFilePaths.Add("Ampar-Ampar_Pisang_3.mid");
            songs.Add(amparPisang);

            Song cikcikPeriuk = new Song
            {
                region = "kalimantan",
                name = "Cik Cik Periuk",
                details = "Cik Cik Periuk (atau Cik Cik Periok) adalah lagu daerah dari daerah Kalimantan Barat tepatnya Kabupaten Sambas. Lagu Cik Cik Periuk bermakna tentang sindiran dari masyarakat Sambas pada zaman dahulu kepada masyarakat luar yang datang ke daerah Sambas"
            };
            cikcikPeriuk.midiFilePaths.Add("Cik_Cik_Periuk_1.mid");
            cikcikPeriuk.midiFilePaths.Add("Cik_Cik_Periuk_2.mid");
            cikcikPeriuk.midiFilePaths.Add("Cik_Cik_Periuk_3.mid");
            songs.Add(cikcikPeriuk);

            Song aekKapuas = new Song
            {
                region = "kalimantan",
                name = "Aek Kapuas",
                details = "Lagu Aek Kapuas menceritakan tentang Aek (Sungai) Kapuas. Sungai Kapuas merupakan ikon Provinsi Kalimantan Barat. Sungai ini adalah sungai terpanjang di Indonesia. Panjang sungai mencapai 1.143 km."
            };
            aekKapuas.midiFilePaths.Add("Aek_Kapuas_1.mid");
            aekKapuas.midiFilePaths.Add("Aek_Kapuas_2.mid");
            aekKapuas.midiFilePaths.Add("Aek_Kapuas_3.mid");
            songs.Add(aekKapuas);

            // Add more songs to kalimantan region as needed
        }
        else if (region == "sulawesi")
        {
            Song angingMammirik = new Song
            {
                region = "sulawesi",
                name = "Anging Mammirik",
                details = "Anging Mammirik adalah sebuah lagu daerah yang berasal dari Sulawesi Selatan dalam Bahasa Makassar. Keindahan melodi lagu ini pun menjadikan lagu 'Anging Mammirik' pengiring sebuah tarian yang bernama sama, yakni Tari Anging Mammirik."
            };
            angingMammirik.midiFilePaths.Add("Anging_Mammirik_1.mid");
            angingMammirik.midiFilePaths.Add("Anging_Mammirik_2.mid");
            angingMammirik.midiFilePaths.Add("Anging_Mammirik_3.mid");
            songs.Add(angingMammirik);

            Song pakarena = new Song
            {
                region = "sulawesi",
                name = "Pakarena",
                details = "Pakarena memiliki makna yang dalam bagi masyarakat Makassar. Isinya sebenarnya adalah perwujudan rasa terima kasih atas ilmu yang diberikan oleh penghuni kayangan kepada manusia penghuni bumi."
            };
            pakarena.midiFilePaths.Add("Pakarena_1.mid");
            pakarena.midiFilePaths.Add("Pakarena_2.mid");
            pakarena.midiFilePaths.Add("Pakarena_3.mid");
            songs.Add(pakarena);

            Song sipatokaan = new Song
            {
                region = "sulawesi",
                name = "Si Patokaan",
                details = "\"Si Patokaan\" mengisahkan tentang kekhawatiran seorang ibu kepada anaknya yang sudah dewasa dan hendak mencari nafkah. Nama Si Patokaan merupakan panggilan seorang anak di daerah Minahasa Sulawesi Utara yang dijadikan fokus utama lagu ini."
            };
            sipatokaan.midiFilePaths.Add("Si_Patokaan_1.mid");
            sipatokaan.midiFilePaths.Add("Si_Patokaan_2.mid");
            sipatokaan.midiFilePaths.Add("Si_Patokaan_3.mid");
            songs.Add(sipatokaan);

            // Add more songs to sulawesi region as needed
        }
        else if (region == "papua")
        {
            Song apuse = new Song
            {
                region = "papua",
                name = "Apuse",
                details = "Apuse adalah sebuah lagu yang berasal dari daerah Kampung Kabouw, Wondiboy, Teluk Wondama, Provinsi Papua Barat. Lagu 'Apuse' diciptakan oleh Tete Mandosir Sarumi dalam bahasa Biak dan dipopulerkan oleh Corry Rumbino, serta dinyanyikan dalam lomba Bintang Radio."
            };
            apuse.midiFilePaths.Add("Apuse_1.mid");
            apuse.midiFilePaths.Add("Apuse_2.mid");
            apuse.midiFilePaths.Add("Apuse_3.mid");
            songs.Add(apuse);

            Song yamko = new Song
            {
                region = "papua",
                name = "Yamko Rambe Yamko",
                details = "Yamko Rambe Yamko adalah suatu lagu daerah yang berasal dari Lembah Grime, yang merupakan wilayah lembah berpenduduk di Kabupaten Jayapura, khususnya merupakan iringan dari tradisi permainan Kasep (Kaseb, Kseep) milik rumpun tiga suku di Lembah Grime, yakni Namblong, Gresi, dan Kemtuk."
            };
            yamko.midiFilePaths.Add("Yamko_Rambe_Yamko_1.mid");
            yamko.midiFilePaths.Add("Yamko_Rambe_Yamko_2.mid");
            yamko.midiFilePaths.Add("Yamko_Rambe_Yamko_3.mid");
            songs.Add(yamko);

            Song sajojo = new Song
            {
                region = "papua",
                name = "Sajojo",
                details = "lagu Sajojo menceritakan tentang seorang gadis yang sangat disayangi oleh kedua orangtuanya. Memiliki wajah yang cantik dan menjadi “kembang desa, membuat banyak pria jatuh cinta kepadanya."
            };
            sajojo.midiFilePaths.Add("sajojo_1.mid");
            sajojo.midiFilePaths.Add("sajojo_2.mid");
            sajojo.midiFilePaths.Add("sajojo_3.mid");
            songs.Add(sajojo);

            // Add more songs to papua region as needed
        }

        // Load the scene after adding the songs
        SceneManager.LoadScene("ChooseSong");
    }

    // Method to clear the list
    private void ClearList()
    {
        songs.Clear();
    }

    public void backBtn()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
