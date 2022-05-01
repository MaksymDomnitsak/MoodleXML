using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Word;

namespace MoodleXML
{
    public partial class Form1 : Form
    {
        private Dictionary<int, string[]> Tasks;
        public Form1()
        {
            InitializeComponent();
        }

        private void ReadFilebtn_Click(object sender, EventArgs e)
        {
            Tasks = new Dictionary<int, string[]>(8);
            int taskflag = 0;
            int completed = 0;
            int lineit = 0;
            string linetask = ".";
            string[] task = new string[7];
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Text files(*.txt)|*.txt";
            try
            {
                if (openfile.ShowDialog() == DialogResult.OK)
            {
                string filepath = openfile.FileName;

                Stream fileStream = openfile.OpenFile();

                using (StreamReader reader = new StreamReader(filepath))
                {
                    
                        while (!String.IsNullOrEmpty(linetask))
                        {
                            while (linetask != "" && linetask != null)
                            {
                                if (taskflag == 0)
                                {
                                    task[lineit] = reader.ReadLine();
                                }
                                else task[lineit] = linetask;
                                linetask = reader.ReadLine();
                                lineit++;
                                taskflag++;
                            }
                            if (lineit != 7)
                            {
                                if(lineit == 1)
                                {
                                    break;
                                }
                                task = new string[7];
                                linetask = ".";
                                taskflag = 0;
                                lineit = 0;
                                MessageBox.Show("Error at reading task. Task skipped.");
                                continue;
                            }
                            Tasks[completed] = task;
                            completed++;
                            if (linetask != null)
                            {
                                task = new string[7];
                                linetask = ".";
                                taskflag = 0;
                                lineit = 0;
                            }
                        }
                    
                }
                if (Tasks.Count != 0)
                {
                    MessageBox.Show("All tasks are read!");
                }
                else
                {
                    throw new Exception();
                }

            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error at reading task/file. Correct mistakes and try again.");
                ExportXML.Enabled = false;
                checkStd.Enabled = false;
                chkdoubleTasks.Enabled = false;
                btnDoc.Enabled = false;
                chkRandOrder.Enabled = false;
                return;
            }
            ExportXML.Enabled = true;
            checkStd.Enabled = true;
            chkdoubleTasks.Enabled = true;
            btnDoc.Enabled = true;
            chkRandOrder.Enabled = true;
        }

        private void RandomOrder_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int rand;
            int[] used;
            Quiz quiz;
            string filepath = ".";
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "XML files(*.xml)|*.xml";
            if (chkdoubleTasks.Checked == true && checkStd.Checked == true)
            {
                quiz = new Quiz(2*Tasks.Count);
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    filepath = savefile.FileName;
                    TextWriter writer = new StreamWriter(filepath);
                    if (chkRandOrder.Checked == true)
                    {
                        used = new int[2 * Tasks.Count];
                        Array.Fill(used, -1);
                        for (int i = 0; i < 2 * Tasks.Count; i += 2)
                        {
                            rand = rnd.Next(Tasks.Count);
                            if (!used.Contains(rand))
                            {
                                used[i] = rand;
                                quiz.question[i] = quiz.FillQuestion(rand, Tasks);
                                quiz.question[i + 1] = quiz.FillChangedQuestion(rand, Tasks);
                            }
                            else
                            {
                                i -= 2;
                                continue;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 2 * Tasks.Count; i += 2)
                        {
                            quiz.question[i] = quiz.FillQuestion(i/2, Tasks);
                            quiz.question[i + 1] = quiz.FillChangedQuestion(i/2, Tasks);
                        }
                    }
                    writer.Write(quiz.ToString());
                    writer.Close();
                }
                else { return; }
            }
            else if(chkdoubleTasks.Checked == true || checkStd.Checked == true)
            {
                quiz = new Quiz(Tasks.Count);
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    filepath = savefile.FileName;
                    TextWriter writer = new StreamWriter(filepath);
                    if (chkRandOrder.Checked == true)
                    {
                        used = new int[Tasks.Count];
                        Array.Fill(used, -1);
                        for (int i = 0; i < Tasks.Count; i++)
                        {
                            rand = rnd.Next(Tasks.Count);
                            if (!used.Contains(rand))
                            {
                                used[i] = rand;
                                if (checkStd.Checked == true) quiz.question[i] = quiz.FillQuestion(rand, Tasks);
                                else quiz.question[i] = quiz.FillChangedQuestion(rand, Tasks);
                            }
                            else
                            {
                                i--;
                                continue;
                            }
                        }
                    } else {
                        for (int i = 0; i < Tasks.Count; i++)
                        {
                            if (checkStd.Checked == true) quiz.question[i] = quiz.FillQuestion(i, Tasks);
                            else quiz.question[i] = quiz.FillChangedQuestion(i, Tasks);
                        }
                    }
                    writer.Write(quiz.ToString());
                    writer.Close();
                } else { return; }
            }
            else
            {
                MessageBox.Show("Choose options for export!");
                return;
            }
            MessageBox.Show(filepath + "\n All tasks completely saved!");
        }

        private void ReturnRemain_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if((chkdoubleTasks.Checked == true && textStartchange.Text == "") || (checkStd.Checked == true && textStartorig.Text == ""))
                {
                    MessageBox.Show("Write start variants before exporting!");
                    return;
                }
                Quiz quiz;
                string classname;
                int change  = -1;
                int standart = -1;
                if (chkdoubleTasks.Checked == true && checkStd.Checked == true)
                {
                    quiz = new Quiz(2 * Tasks.Count);
                    standart = Convert.ToInt32(textStartorig.Text);
                    change = Convert.ToInt32(textStartchange.Text);
                    for (int i = 0; i < Tasks.Count; i++)
                    {
                        quiz.question[i] = quiz.ConvertQuestionsforDoc(i,Tasks,false);
                        quiz.question[Tasks.Count+i] = quiz.ConvertQuestionsforDoc(i,Tasks,true);
                    }
                }
                else if (chkdoubleTasks.Checked == true || checkStd.Checked == true)
                {
                    quiz = new Quiz(Tasks.Count);
                    for (int i = 0; i < Tasks.Count; i++)
                    {
                        if (checkStd.Checked == true)
                        {
                            quiz.question[i] = quiz.ConvertQuestionsforDoc(i, Tasks, false);
                            standart = Convert.ToInt32(textStartorig.Text);
                        }
                        else
                        {
                            quiz.question[i] = quiz.ConvertQuestionsforDoc(i, Tasks, true);
                            change = Convert.ToInt32(textStartchange.Text);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Choose options for export!");
                    return;
                }
                progressBar.Visible = true;
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                winword.Visible = false;
                Document document = winword.Documents.Add();
                int countparagraphs = 1;
                Microsoft.Office.Interop.Word.Range rng;
                for (int i = 0; i < quiz.count; i++)
                {
                    rng = document.Paragraphs[countparagraphs].Range;
                    document.Paragraphs[document.Paragraphs.Count].Alignment=WdParagraphAlignment.wdAlignParagraphCenter;
                    document.Paragraphs[document.Paragraphs.Count].SpaceAfter = 6.0F;
                    if(chkdoubleTasks.Checked == true && checkStd.Checked == true)
                    {
                        if (i >= Tasks.Count)
                        {
                            rng.Text = "Варіант " + (change++);
                        } else rng.Text = "Варіант " + (standart++);
                    }
                    else
                    {
                        if (checkStd.Checked == true)
                        {
                            rng.Text = "Варіант " + (standart++);
                        }
                        else
                        {
                            rng.Text = "Варіант " + (change++);
                        }
                    }
                    rng.Font.Name = "Arial";
                    rng.Font.Size = 11.0F;
                    rng.Bold = 1;
                    rng.Select();
                    rng.InsertParagraphAfter();
                    document.Paragraphs[document.Paragraphs.Count].Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    rng = document.Paragraphs[document.Paragraphs.Count].Range;
                    rng.Text = quiz.question[i].Questiontext;
                    rng.Bold = 0;
                    rng.Select();
                    rng.InsertParagraphAfter();
                    document.Paragraphs[document.Paragraphs.Count].Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    rng = document.Paragraphs[document.Paragraphs.Count].Range;
                    rng.Text = "Код програми";
                    rng.Bold = 1;
                    rng.Select();
                    rng.InsertParagraphAfter();
                    document.Paragraphs[document.Paragraphs.Count].Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    rng = document.Paragraphs[document.Paragraphs.Count].Range;
                    rng.Text = quiz.question[i].Responsetemplate;
                    rng.Bold = 0;
                    countparagraphs = document.Paragraphs.Count;
                    if(i == quiz.count - 1)
                    {
                        progressBar.Increment(50 - progressBar.Value);
                        continue;
                    }
                    progressBar.Value += (int)Math.Round(50.0/quiz.count);
                }
                int pbar = 1;
                for (int i = 1; i <= document.Paragraphs.Count; i++)
                {

                    rng = document.Paragraphs[i].Range;
                    rng.Find.ClearFormatting();
                    if (rng.Find.Execute("Визначити оператори:", Forward: true) == true)
                    {
                        rng.Select();
                        rng.Bold = 1;
                        continue;
                    }
                    if (rng.Find.Execute("Визначити функції:", Forward: true) == true)
                    {
                        rng.Select();
                        rng.Bold = 1;
                        continue;
                    }
                    if (rng.Find.Execute("Написати функцію main()", Forward: true) == true)
                    {
                        rng.Select();
                        rng.Bold = 1;
                        continue;
                    }
                    if(rng.Text.ToString().Contains("Створити клас"))
                    {
                        classname = rng.Text.ToString().Substring(13, rng.Text.ToString().IndexOf(".") - 13);
                        rng.Find.Execute(classname, Forward: true);
                        rng.Select();
                        rng.Bold = 1;
                    }
                    if(i == document.Paragraphs.Count)
                    {
                        progressBar.Value = 0;
                        progressBar.Visible = false;
                        continue;
                    }
                    if((int)Math.Round((double)i / document.Paragraphs.Count*100.0) == pbar && pbar < 50)
                    {
                        progressBar.Value += 1;
                        pbar += 1;
                    }

                }
                rng = document.Content;
                rng.PageSetup.TopMargin = 25;
                rng.PageSetup.BottomMargin = 25;
                rng.PageSetup.LeftMargin = 25;
                rng.PageSetup.RightMargin = 25;
                document.Paragraphs.SpaceBefore = 0.0F;
                document.Paragraphs.SpaceAfter = 0.0F;
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Doc files (*.doc)|*.doc";
                save.Filter += "|Docx files (*.docx)|*.docx";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    String filename = save.FileName;
                    document.SaveAs2(@filename);
                    document.Close();
                    winword.Quit();
                    MessageBox.Show("Tasks saved!");
                }
                else
                {
                    document.Close();
                    winword.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkStd_CheckedChanged(object sender, EventArgs e)
        {
            if (checkStd.Checked == true)
            {
                textStartorig.Enabled = true;
                textStartorig.Text = "1";
                if(chkdoubleTasks.Checked == true)
                {
                    textStartchange.Text = (Tasks.Count + 1).ToString();
                }
            }
            else
            {
                if (chkdoubleTasks.Checked == true)
                {
                    textStartchange.Text = "1";
                }
                textStartorig.Text = "";
                textStartorig.Enabled = false;
            }
        }

        private void chkdoubleTasks_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdoubleTasks.Checked == true)
            {
                textStartchange.Enabled = true;
                textStartchange.Text = "1";
                if (checkStd.Checked == true)
                {
                    textStartchange.Text = (Tasks.Count + 1).ToString();
                }
            }
            else
            {
                textStartchange.Text = "";
                textStartchange.Enabled = false;
            }
        }

        private void textStartorig_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Back))
            {
                return;
            }
            if (e.KeyChar < '0' | e.KeyChar > '9')
            {
                e.Handled = true;
            }
        }

        private void textStartchange_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == ((char)Keys.Back))
            {
                return;
            }
            if (e.KeyChar < '0' | e.KeyChar > '9')
            {
                e.Handled = true;
            }
        }
    }
}
