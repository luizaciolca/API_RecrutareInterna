namespace API_RecrutareInterna.Helpers
{
    public class ErrorMessageEnum
    {
        public class Job
        {
            public const string NotFound = "Niciun job găsit în tabel!";
            public const string NotFoundById = "Job-ul cu ID-ul furnizat nu există";
            public const string BadRequest = "Formatul transmis nu este corect.";
            public const string ZeroUpdatesToSave = "Nu există actualizări de salvat pentru job.";
            public const string StartEndDateError = "Data de sfârșit nu poate fi anterioară datei de început.";
            public const string PozitieExistsError = "Pozitia deja există.";
        }



        public class Echipa
        {
            public const string NotFound = "Niciun departament găsit în tabel!";
            public const string NotFoundById = "Departamentul cu ID-ul furnizat nu există";
            public const string BadRequest = "Formatul transmis nu este corect.";
            public const string ZeroUpdatesToSave = "Nu există actualizări de salvat pentru echipa.";
            public const string StartEndDateError = "Data de sfârșit nu poate fi anterioară datei de început.";
            public const string DepartamentExistsError = "Departamentul deja există.";
        }

        public class Beneficii
        {
            public const string NotFound = "Niciun beneficiu găsit în tabel!";
            public const string NotFoundById = "Beneficiul cu ID-ul furnizat nu există";
            public const string BadRequest = "Formatul transmis nu este corect.";
            public const string ZeroUpdatesToSave = "Nu există actualizări de salvat pentru beneficiu.";
            public const string StartEndDateError = "Data de sfârșit nu poate fi anterioară datei de început.";
            public const string TitluExistsError = "Beneficiul deja există.";
        }

        public class ProcesInterviu
        {
            public const string NotFound = "Niciun proces de interviere găsit în tabel!";
            public const string NotFoundById = "Procesul de interviere cu ID-ul furnizat nu există";
            public const string BadRequest = "Formatul transmis nu este corect.";
            public const string ZeroUpdatesToSave = "Nu există actualizări de salvat pentru interviu.";
            public const string StartEndDateError = "Data de sfârșit nu poate fi anterioară datei de început.";
            public const string TitluExistsError = "Procesul de interviere deja există.";
        }

        public class Proiect
        {
            public const string NotFound = "Niciun proiect găsit în tabel!";
            public const string NotFoundById = "Proiectul cu ID-ul furnizat nu există";
            public const string BadRequest = "Formatul transmis nu este corect.";
            public const string ZeroUpdatesToSave = "Nu există actualizări de salvat pentru proiect.";
            public const string StartEndDateError = "Data de sfârșit nu poate fi anterioară datei de început.";
            public const string ProiectItemExistsError = "Proiectul deja există.";
        }
    }
}

    

