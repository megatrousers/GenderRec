using GenderRec01.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GenderRec01
{
   

    public enum BatchType { TRAINING, TESTING };
    class Supervisor
    {
        public const int RESULTS_PROP_NUM = 3;
        const string DEFAULT_BATCH_PREFIX = "Untitled_batch_";

        private static Supervisor instance;
        ImageLoader il;
        Dictionary<string, SubjectBatch> subjectBatches;
        Dictionary<string, Dictionary<string, string[]>> resultBatches;
        string focusBatchName;
        Dictionary<string, Subject> focusSubjects;
        int assignBatchNum;

        private Supervisor()
        {
            il = new ImageLoader();
            subjectBatches = new Dictionary<string, SubjectBatch>();
            resultBatches = new Dictionary<string, Dictionary<string, string[]>>();
            focusSubjects = new Dictionary<string, Subject>();
            assignBatchNum = 0;
            focusBatchName = DEFAULT_BATCH_PREFIX + assignBatchNum;
        }

        public static Supervisor Instance
        {
            get 
            {
                if(instance == null)
                {
                    instance = new Supervisor();
                }
                return instance;
            }
        }

        public void LoadImageSet(string[] fullPathFileNames, BatchType batchType)
        {
            int slashPos = 0;
            string[] fileNames = new string[fullPathFileNames.Count()];
            slashPos = fullPathFileNames[0].LastIndexOf("\\");
            string path = fullPathFileNames[0].Substring(0, slashPos+1);
            for(int i = 0; i < fullPathFileNames.Count(); i++)
            {
                fileNames[i] = fullPathFileNames[i].Substring(slashPos+1);
            }
            LoadImageSet(path, fileNames, batchType);
        }
        public void LoadImageSet(string path, string[] fileNames, BatchType batchType)
        {
            //Get a list of images from source
            List<BitmapImage>  imageBatch = il.Load(path, fileNames);
           
            //Create a dictionary of subjects from the list of images
            if( imageBatch != null )
            {
                SubjectBatch subjects = new SubjectBatch();
                subjects.Name = DEFAULT_BATCH_PREFIX + assignBatchNum++;
                    
                for (int i = 0; i < imageBatch.Count; i++)
	            {
                    Subject subject = new Subject();
                    subject.BaseImage = imageBatch.ElementAt(i);
                    subject.Path = path;
                    subject.Name = fileNames[i];
                    subject.Gender = GenderType.UNKNOWN;
                    subjects.Add(subject);
	            }

                //Add the collection of new subjects to the batch list
                subjectBatches.Add(subjects.Name, subjects);
                focusBatchName = subjects.Name;
                if(!FocusSubjects.Keys.Contains(subjects.Name))
                {
                    focusSubjects.Add(focusBatchName, subjects.Subjects.Values.Last());
                }
                else
                {
                    focusSubjects[focusBatchName] = subjects.Subjects.Values.Last();
                }
                
            }
        }//end of LoadSet

        public bool ReduceBatchSimple()
        {

            return true;
        }

        public bool reduceBatchPCA()
        {

            return true;
        }

        public void SvmTrain(int trainBatchName)
        {
            
        }

        public void SvmClassify(string classifyBatchName)
        {
            SubjectBatch testBatch;
            subjectBatches.TryGetValue(classifyBatchName, out testBatch);

            foreach (KeyValuePair<string, Subject> subject in testBatch.Subjects)
            {
                subject.Value.Gender = GenderType.MALE;
            }

        }

        private void AppendResults(SubjectBatch testBatch)
        {
            Dictionary<string, string[]> resultBatch = new Dictionary<string, string[]>();
            string[] result;
            foreach (KeyValuePair<string, Subject> subject in testBatch.Subjects)
            {
                result = new string[RESULTS_PROP_NUM];
                result[0] = subject.Value.Name;
                result[1] = subject.Value.Path;
                result[2] = subject.Value.Gender.ToString();

                resultBatch.Add(subject.Value.Name, result);
            }
            resultBatches.Add(testBatch.Name, resultBatch);

        }

        public void ChangeFocusSubject(string newFocusSubjectName)
        {
            Subject newFocusSubject; 
            subjectBatches[focusBatchName].Subjects.TryGetValue(newFocusSubjectName, out newFocusSubject);
            focusSubjects[focusBatchName] = newFocusSubject;
        }

#region Properties
        public Dictionary<string, SubjectBatch> SubjectBatches
        {
            get { return subjectBatches; }
        }
        public string FocusBatch
        {
            get {return focusBatchName; }
        }
        public Dictionary<string, Subject> FocusSubjects
        {
            get { return focusSubjects; }
        }
#endregion Properties

    }
}
