using System.Collections.Generic;

namespace MoodleXML
{
    public class Quiz
    {
        public class Question
        {
            private string name;
            private string questiontext;
            private string responsetemplate = "<![CDATA[<ol> <li>class ...</li><li>{&nbsp;</li><li><b>private:</b></li><li>// приватні члени дані класу</li><li><br></li><li><br></li><li><br></li>" +
                        "<li>// приватні функції члени класу(тільки, якщо Вам для чогось потрібні)</li>" +
                        "<li><br></li><li><br></li><li><b>public:</b></li><li>// конструктори(тільки заголовки)</li><li><br></li><li><br></li><li>// деструктор</li>" +
                        "<li><br></li><li><br></li><li><b>// прототипи членів класу: функції та оператори</b></li>";
            public double defaultgrade = 10.0;

            public string Questiontext { get => questiontext; internal set => questiontext = value; }
            public string Name { get => name; internal set => name = value; }
            public string Responsetemplate { get => responsetemplate; internal set => responsetemplate = value; }

            public Question() { }

        }

        public Question[] question;
        public int count;

        public Quiz()
        {
            question = new Question[1];
            count = 1;
        }

        public Quiz(int capacity)
        {
            question = new Question[capacity];
            count = capacity;
        }

        private readonly string[] inout = new string[2] { "Для виведення інформації про об'єкт визначити дружній оператор <<, а для введення – член-функцію input().<br>",
            "Для введення інформації про об'єкт визначити дружній оператор >>, а для виведення – член-функцію output().<br>" };
        private readonly string aboutmain = "<b>Написати функцію main()</b>, в якій створити об'єкти класу і викликати всі створені оператори та функції. <br>\n\t\t\t\t" +
            "Тіла функцій розміщати за межами інтерфейсної частини класу. <br>\n\t\t\t\tНумерацію рядків в інтерфейсній частині класу не змінювати. Свій код писати нежирним шрифтом.";
        private readonly string aboutmainforDoc = "Написати функцію main(), в якій створити об'єкти класу і викликати всі створені оператори та функції. " +
            "Тіла функцій розміщати за межами інтерфейсної частини класу.\n";
        public Question FillQuestion(int num, Dictionary<int, string[]> Tasks)
        {
            string proto = "";
            string body = "";
            string protofriend = "";
            string bodyfriend = "";
            string[] task = new string[7];
            Tasks[num].CopyTo(task, 0);
            Question q = new Question();
            q.Name = "Завдання.";
            q.Questiontext = "<![CDATA[";
            for (int i = 0; i < Tasks[num].Length; i++)
            {
                q.Questiontext += task[i] + "<br>\n\t\t\t\t";
                if (i == 0)
                {
                    if (num % 2 == 0)
                    {
                        q.Questiontext += inout[0] + "\n\t\t\t\t";
                        proto += "<li>//input() - введення об'єкта</li><li><br></li><li><br></li>";
                        body += "//input() - введення об'єкта<br> <br> <br> <br>";
                        protofriend += "<li>// оператор <<</li><li><br></li><li><br></li>";
                        bodyfriend += "// оператор <<<br> <br> <br> <br>";
                    }
                    else
                    {
                        q.Questiontext += inout[1] + "\n\t\t\t\t";
                        proto += "<li>//output() - виведення об'єкта</li><li><br></li><li><br></li>";
                        body += "//output() - виведення об'єкта<br> <br> <br> <br>";
                        protofriend += "<li>// оператор >></li><li><br></li><li><br></li>";
                        bodyfriend += "// оператор >><br> <br> <br> <br>";
                    }
                    continue;
                }
                if (task[i].Contains("дружня функція"))
                {
                    task[i] = task[i].Remove(0, 1);
                    task[i] = task[i].Replace("дружня функція", "");
                    if (task[i].IndexOf("(") != -1)
                    {
                        task[i] = task[i].Remove(task[i].IndexOf("("));
                    }
                    protofriend += "<li>//" + task[i] + "</li><li><br></li><li><br></li>";
                    bodyfriend += "//" + task[i] + "<br> <br> <br> <br>";
                }
                else if (task[i].Contains("дружній оператор"))
                {
                    task[i] = task[i].Remove(0, 1);
                    task[i] = task[i].Remove(task[i].Length - 1, 1);
                    task[i] = task[i].Replace("дружній оператор", "");
                    if (task[i].IndexOf("(") != -1)
                    {
                        task[i] = task[i].Remove(task[i].IndexOf("("));
                    }
                    if (task[i].Length < 10)
                    {
                        protofriend += "<li>//" + task[i] + "оператор</li><li><br></li><li><br></li>";
                        bodyfriend += "//" + task[i] + "оператор<br> <br> <br> <br>";
                    }
                    else
                    {
                        protofriend += "<li>//" + task[i] + "</li><li><br></li><li><br></li>";
                        bodyfriend += "//" + task[i] + "<br> <br> <br> <br>";
                    }
                }
                else if (task[i].Contains("-член класу"))
                {
                    task[i] = task[i].Remove(0, 1);
                    task[i] = task[i].Replace("функція-член класу", "");
                    if (task[i].Contains("оператор"))
                    {
                        task[i] = task[i].Remove(task[i].Length - 1, 1);
                        task[i] = task[i].Replace("оператор-член класу", "");
                        if (task[i].IndexOf("(") != -1)
                        {
                            task[i] = task[i].Remove(task[i].IndexOf("("));
                        }
                        if (task[i].Length < 10)
                        {
                            proto += "<li>//" + task[i] + "оператор</li><li><br></li><li><br></li>";
                            body += "//" + task[i] + "оператор<br> <br> <br> <br>";
                        }
                        else
                        {
                            proto += "<li>//" + task[i] + "</li><li><br></li><li><br></li>";
                            body += "//" + task[i] + "<br> <br> <br> <br>";
                        }
                        continue;
                    }
                    if (task[i].IndexOf("(") != -1)
                    {
                        task[i] = task[i].Remove(task[i].IndexOf("("));
                    }
                    proto += "<li>//" + task[i] + "</li><li><br></li><li><br></li>";
                    body += "//" + task[i] + "<br> <br> <br> <br>";
                }
                else
                {
                    if (!task[i].Contains("дружн") && !task[i].Contains("-член класу"))
                    {
                        continue;
                    }
                    task[i] = task[i].Remove(0, 1);
                    proto += "<li>//" + task[i] + "</li><li><br></li><li><br></li>";
                    body += "//" + task[i] + "<br> <br> <br> <br>";
                }
            }
            q.Responsetemplate += proto + "<li><b>//прототипи дружніх операторів і функцій</b></li>" + protofriend +
            "<li>};</li></ol><br><br><b>//тіла конструкторів і деструктора</b><br><br><br><br><b>// тіла членів класу: функції та оператори</b><br>" + body +
            "<b>// тіла дружніх операторів і функцій </b><br>" + bodyfriend + "<b>int main()</b><br>{<br>// створення об'єктів написаного класу" +
                "<br><br><br>// виклик функцій-членів класу<br><br><br>// виклик дружніх функцій<br><br><br>// виклик операторів<br><br><br>&nbsp;&nbsp; return 0;<br>}]]>";
            q.Questiontext += aboutmain + "]]>";
            q.Questiontext = q.Questiontext.Insert(22, "<b>");
            q.Questiontext = q.Questiontext.Insert(q.Questiontext.IndexOf("."), "</b>");
            q.Questiontext = q.Questiontext.Replace("Визначити функції", "<b>Визначити функції</b>");
            q.Questiontext = q.Questiontext.Replace("Визначити оператори", "<b>Визначити оператори</b>");

            return q;
        }

        public Question FillChangedQuestion(int num, Dictionary<int, string[]> Tasks)
        {
            string proto = "";
            string body = "";
            string protofriend = "";
            string bodyfriend = "";
            string[] task = new string[7];
            Tasks[num].CopyTo(task, 0);
            Question q = new Question();
            q.Name = "Завдання.";
            q.Questiontext = "<![CDATA[";
            for (int i = 0; i < Tasks[num].Length; i++)
            {
                if (i == 0)
                {
                    if (num % 2 == 0)
                    {
                        q.Questiontext += task[i] + "<br>\n\t\t\t\t";
                        q.Questiontext += inout[1] + "\n\t\t\t\t";
                        proto += "<li>//output() - виведення об'єкта</li><li><br></li><li><br></li>";
                        body += "//output() - виведення об'єкта<br> <br> <br> <br>";
                        protofriend += "<li>// оператор >></li><li><br></li><li><br></li>";
                        bodyfriend += "// оператор >><br> <br> <br> <br>";
                    }
                    else
                    {
                        q.Questiontext += task[i] + "<br>\n\t\t\t\t";
                        q.Questiontext += inout[0] + "\n\t\t\t\t";
                        proto += "<li>//input() - введення об'єкта</li><li><br></li><li><br></li>";
                        body += "//input() - введення об'єкта<br> <br> <br> <br>";
                        protofriend += "<li>// оператор <<</li><li><br></li><li><br></li>";
                        bodyfriend += "// оператор <<<br> <br> <br> <br>";

                    }
                    continue;
                }
                if (task[i].Contains("дружня функція"))
                {
                    task[i] = task[i].Replace("дружня функція", "функція-член класу");
                    q.Questiontext += task[i] + "<br>\n\t\t\t\t";
                    task[i] = task[i].Remove(0, 1);
                    task[i] = task[i].Replace("функція-член класу", "");
                    if (task[i].IndexOf("(") != -1)
                    {
                        task[i] = task[i].Remove(task[i].IndexOf("("));
                    }
                    proto += "<li>//" + task[i] + "</li><li><br></li><li><br></li>";
                    body += "//" + task[i] + "<br> <br> <br> <br>";
                }
                else if (task[i].Contains("дружній оператор"))
                {
                    task[i] = task[i].Replace("дружній оператор", "оператор-член класу");
                    q.Questiontext += task[i] + "<br>\n\t\t\t\t";
                    task[i] = task[i].Remove(0, 1);
                    task[i] = task[i].Remove(task[i].Length - 1, 1);
                    task[i] = task[i].Replace("оператор-член класу", "");
                    if (task[i].IndexOf("(") != -1)
                    {
                        task[i] = task[i].Remove(task[i].IndexOf("("));
                    }
                    if (task[i].Length >= 10)
                    {
                        proto += "<li>//" + task[i] + "</li><li><br></li><li><br></li>";
                        body += "//" + task[i] + "<br> <br> <br> <br>";
                    }
                    else
                    {
                        proto += "<li>//" + task[i] + "оператор</li><li><br></li><li><br></li>";
                        body += "//" + task[i] + "оператор<br> <br> <br> <br>";
                    }
                }
                else if (task[i].Contains("-член класу"))
                {
                    if (task[i].Contains("оператор"))
                    {
                        task[i] = task[i].Replace("оператор-член класу", "дружній оператор");
                        q.Questiontext += task[i] + "<br>\n\t\t\t\t";
                        task[i] = task[i].Replace("дружній оператор", "");
                        task[i] = task[i].Remove(0, 1);
                        task[i] = task[i].Remove(task[i].Length - 1, 1);
                        if (task[i].IndexOf("(") != -1)
                        {
                            task[i] = task[i].Remove(task[i].IndexOf("("));
                        }
                        if (task[i].Length < 10)
                        {
                            protofriend += "<li>//" + task[i] + "оператор</li><li><br></li><li><br></li>";
                            bodyfriend += "//" + task[i] + "оператор<br> <br> <br> <br>";
                        }
                        else
                        {
                            protofriend += "<li>//" + task[i] + "</li><li><br></li><li><br></li>";
                            bodyfriend += "//" + task[i] + "<br> <br> <br> <br>";
                        }
                    }
                    else
                    {
                        task[i] = task[i].Replace("функція-член класу", "дружня функція");
                        q.Questiontext += task[i] + "<br>\n\t\t\t\t";
                        task[i] = task[i].Replace("дружня функція", "");
                        task[i] = task[i].Remove(0, 1);
                        if (task[i].IndexOf("(") != -1)
                        {
                            task[i] = task[i].Remove(task[i].IndexOf("("));
                        }
                        protofriend += "<li>//" + task[i] + "</li><li><br></li><li><br></li>";
                        bodyfriend += "//" + task[i] + "<br> <br> <br> <br>";
                    }
                }
                else
                {
                    if (!task[i].Contains("дружн") && !task[i].Contains("-член класу"))
                    {
                        q.Questiontext += task[i] + "<br>\n\t\t\t\t";
                        continue;
                    }
                    task[i] = task[i].Remove(0, 1);
                    proto += "<li>//" + task[i] + "</li><li><br></li><li><br></li>";
                    body += "//" + task[i] + "<br> <br> <br> <br>";
                }
            }
            q.Responsetemplate += proto + "<li><b>//прототипи дружніх операторів і функцій</b></li>" + protofriend +
                "<li>};</li></ol><br><br><b>//тіла конструкторів і деструктора</b><br><br><br><br><b>// тіла членів класу: функції та оператори</b><br>" + body +
                "<b>// тіла дружніх операторів і функцій </b><br>" + bodyfriend + "<b>int main()</b><br>{<br>// створення об'єктів написаного класу" +
                "<br><br><br>// виклик функцій-членів класу<br><br><br>// виклик дружніх функцій<br><br><br>// виклик операторів<br><br><br>&nbsp;&nbsp; return 0;<br>}]]>";
            q.Questiontext += aboutmain + "]]>";
            q.Questiontext = q.Questiontext.Insert(22, "<b>");
            q.Questiontext = q.Questiontext.Insert(q.Questiontext.IndexOf("."), "</b>");
            q.Questiontext = q.Questiontext.Replace("Визначити функції", "<b>Визначити функції</b>");
            q.Questiontext = q.Questiontext.Replace("Визначити оператори", "<b>Визначити оператори</b>");
            return q;
        }

        public Question ConvertQuestionsforDoc(int num, Dictionary<int, string[]> Dict, bool change)
        {
            string proto = "";
            string body = "";
            string protofriend = "";
            string bodyfriend = "";
            string[] task = new string[7];
            Dict[num].CopyTo(task, 0);
            Question q = new Question();
            q.Responsetemplate = "class ...\n{\nprivate:\n// приватні члени дані класу\n\n\n\n" +
                        "// приватні функції члени класу(тільки, якщо Вам для чогось потрібні)\n\n\n" +
                        "public:\n// конструктори(тільки заголовки)\n\n\n// деструктор\n\n\n" +
                        "// прототипи членів класу: функції та оператори\n";
            if (change == true)
            {
                for (int i = 0; i < Dict[num].Length; i++)
                {
                    if (i == 0)
                    {
                        if (num % 2 == 0)
                        {
                            q.Questiontext += task[i] + " ";
                            q.Questiontext += inout[1].Replace("<br>", "") + "\n";
                            proto += "// output() - виведення об'єкта\n\n";
                            body += "// output() - виведення об'єкта\n\n\n\n\n";
                            protofriend += "// оператор >>\n\n";
                            bodyfriend += "// оператор >>\n\n\n\n\n";
                        }
                        else
                        {
                            q.Questiontext += task[i] + " ";
                            q.Questiontext += inout[0].Replace("<br>", "") + "\n";
                            proto += "// input() - введення об'єкта\n\n";
                            body += "// input() - введення об'єкта\n\n\n\n\n";
                            protofriend += "// оператор <<\n\n";
                            bodyfriend += "// оператор <<\n\n\n\n\n";
                        }
                        continue;
                    }
                    if (task[i].Contains("дружня функція"))
                    {
                        task[i] = task[i].Replace("дружня функція", "функція-член класу");
                        q.Questiontext += task[i] + "\n";
                        task[i] = task[i].Remove(0, 1);
                        task[i] = task[i].Replace("функція-член класу", "");
                        if (task[i].IndexOf("(") != -1)
                        {
                            task[i] = task[i].Remove(task[i].IndexOf("("));
                        }
                        proto += "//" + task[i] + "\n\n";
                        body += "//" + task[i] + "\n\n\n\n\n";
                    }
                    else if (task[i].Contains("дружній оператор"))
                    {
                        task[i] = task[i].Replace("дружній оператор", "оператор-член класу");
                        q.Questiontext += task[i] + "\n";
                        task[i] = task[i].Remove(0, 1);
                        task[i] = task[i].Remove(task[i].Length - 1, 1);
                        task[i] = task[i].Replace("оператор-член класу", "");
                        if (task[i].IndexOf("(") != -1)
                        {
                            task[i] = task[i].Remove(task[i].IndexOf("("));
                        }
                        if (task[i].Length < 10)
                        {
                            proto += "//" + task[i] + "оператор\n\n";
                            body += "//" + task[i] + "оператор\n\n\n\n\n";
                        }
                        else
                        {
                            proto += "//" + task[i] + "\n\n";
                            body += "//" + task[i] + "\n\n\n\n\n";
                        }
                    }
                    else if (task[i].Contains("-член класу"))
                    {
                        if (task[i].Contains("оператор"))
                        {

                            task[i] = task[i].Replace("оператор-член класу", "дружній оператор");
                            q.Questiontext += task[i] + "\n";
                            task[i] = task[i].Replace("дружній оператор", "");
                            task[i] = task[i].Remove(0, 1);
                            task[i] = task[i].Remove(task[i].Length - 1, 1);
                            if (task[i].IndexOf("(") != -1)
                            {
                                task[i] = task[i].Remove(task[i].IndexOf("("));
                            }
                            if (task[i].Length < 10)
                            {
                                protofriend += "//" + task[i] + "оператор\n\n";
                                bodyfriend += "//" + task[i] + "оператор\n\n\n\n\n";
                            }
                            else
                            {
                                protofriend += "//" + task[i] + "\n\n";
                                bodyfriend += "//" + task[i] + "\n\n\n\n\n";
                            }
                        }
                        else
                        {
                            task[i] = task[i].Replace("функція-член класу", "дружня функція");
                            q.Questiontext += task[i] + "\n";
                            task[i] = task[i].Replace("дружня функція", "");
                            task[i] = task[i].Remove(0, 1);
                            if (task[i].IndexOf("(") != -1)
                            {
                                task[i] = task[i].Remove(task[i].IndexOf("("));
                            }
                            protofriend += "//" + task[i] + "\n\n";
                            bodyfriend += "//" + task[i] + "\n\n\n\n\n";
                        }
                    }
                    else
                    {
                        if (!task[i].Contains("дружн") && !task[i].Contains("-член класу"))
                        {
                            q.Questiontext += task[i] + "\n";
                            continue;
                        }
                        task[i] = task[i].Remove(0, 1);
                        proto += "//" + task[i] + "\n\n";
                        body += "//" + task[i] + "\n\n\n\n\n";
                    }
                }
                q.Responsetemplate += proto + "// прототипи дружніх операторів і функцій\n" + protofriend +
                    "};\n\f" + "// тіла конструкторів і деструктора\n\n\n\n\n// тіла членів класу: функції та оператори\n" + body +
                    "// тіла дружніх операторів і функцій \n" + bodyfriend + "int main(){\n// створення об'єктів написаного класу\n\n\n" +
                    "// виклик функцій-членів класу\n\n\n// виклик дружніх функцій\n\n\n// виклик операторів\n\n\n\n    return 0;}\f\n";
                q.Questiontext += aboutmainforDoc;
            }
            else
            {
                for (int i = 0; i < Dict[num].Length; i++)
                {
                    if (i == 0)
                    {
                        if (num % 2 == 0)
                        {
                            q.Questiontext += task[i] + " ";
                            q.Questiontext += inout[0].Replace("<br>", "") + "\n";
                            proto += "// input() - введення об'єкта\n\n";
                            body += "// input() - введення об'єкта\n\n\n\n\n";
                            protofriend += "// оператор <<\n\n";
                            bodyfriend += "// оператор <<\n\n\n\n\n";
                        }
                        else
                        {
                            q.Questiontext += task[i] + " ";
                            q.Questiontext += inout[1].Replace("<br>", "") + "\n";
                            proto += "// output() - виведення об'єкта\n\n";
                            body += "// output() - виведення об'єкта\n\n\n\n\n";
                            protofriend += "// оператор >>\n\n";
                            bodyfriend += "// оператор >>\n\n\n\n\n";

                        }
                        continue;
                    }
                    if (task[i].Contains("дружня функція"))
                    {
                        q.Questiontext += task[i] + "\n";
                        task[i] = task[i].Remove(0, 1);
                        task[i] = task[i].Replace("дружня функція", "");
                        if (task[i].IndexOf("(") != -1)
                        {
                            task[i] = task[i].Remove(task[i].IndexOf("("));
                        }
                        protofriend += "//" + task[i] + "\n\n";
                        bodyfriend += "//" + task[i] + "\n\n\n\n\n";
                    }
                    else if (task[i].Contains("дружній оператор"))
                    {
                        q.Questiontext += task[i] + "\n";
                        task[i] = task[i].Remove(0, 1);
                        task[i] = task[i].Remove(task[i].Length - 1, 1);
                        task[i] = task[i].Replace("дружній оператор", "");
                        if (task[i].IndexOf("(") != -1)
                        {
                            task[i] = task[i].Remove(task[i].IndexOf("("));
                        }
                        if (task[i].Length < 10)
                        {
                            protofriend += "//" + task[i] + "оператор\n\n";
                            bodyfriend += "//" + task[i] + "оператор\n\n\n\n\n";
                        }
                        else
                        {
                            protofriend += "//" + task[i] + "\n\n";
                            bodyfriend += "//" + task[i] + "\n\n\n\n\n";
                        }
                    }
                    else if (task[i].Contains("-член класу"))
                    {
                        q.Questiontext += task[i] + "\n";
                        if (task[i].Contains("оператор"))
                        {
                            task[i] = task[i].Replace("оператор-член класу", "");
                            task[i] = task[i].Remove(0, 1);
                            task[i] = task[i].Remove(task[i].Length - 1, 1);
                            if (task[i].IndexOf("(") != -1)
                            {
                                task[i] = task[i].Remove(task[i].IndexOf("("));
                            }
                            if (task[i].Length < 10)
                            {
                                proto += "//" + task[i] + "оператор\n\n";
                                body += "//" + task[i] + "оператор\n\n\n\n\n";
                            }
                            else
                            {
                                proto += "//" + task[i] + "\n\n";
                                body += "//" + task[i] + "\n\n\n\n\n";
                            }
                        }
                        else
                        {
                            task[i] = task[i].Replace("функція-член класу", "");
                            task[i] = task[i].Remove(0, 1);
                            if (task[i].IndexOf("(") != -1)
                            {
                                task[i] = task[i].Remove(task[i].IndexOf("("));
                            }
                            proto += "//" + task[i] + "\n\n";
                            body += "//" + task[i] + "\n\n\n\n\n";
                        }
                    }
                    else
                    {
                        if (!task[i].Contains("дружн") && !task[i].Contains("-член класу"))
                        {
                            q.Questiontext += task[i] + "\n";
                            continue;
                        }
                        q.Questiontext += task[i] + "\n";
                        task[i] = task[i].Remove(0, 1);
                        proto += "//" + task[i] + "\n\n";
                        body += "//" + task[i] + "\n\n\n\n\n";
                    }
                }
                q.Responsetemplate += proto + "// прототипи дружніх операторів і функцій\n" + protofriend +
                    "};\n\f// тіла конструкторів і деструктора\n\n\n\n\n// тіла членів класу: функції та оператори\n" + body +
                    "// тіла дружніх операторів і функцій \n" + bodyfriend + "int main(){\n// створення об'єктів написаного класу\n\n\n" +
                    "// виклик функцій-членів класу\n\n\n// виклик дружніх функцій\n\n\n// виклик операторів\n\n\n\n    return 0;}\f\n";
                q.Questiontext += aboutmainforDoc;
            }
            return q;
        }

        public override string ToString()
        {
            string allquestions = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<quiz xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n\t";
            for (int i = 0; i < this.count; i++)
            {
                allquestions += "<question type=\"essay\">\n\t\t<name>\n\t\t\t<text>" + question[i].Name + "</text>\n\t\t</name>\n\t\t<questiontext>\n\t\t\t<text>" +
                    question[i].Questiontext + "</text>\n\t\t</questiontext>\n\t\t<responsetemplate>\n\t\t\t<text>" + question[i].Responsetemplate +
                    "</text>\n\t\t</responsetemplate>\n\t\t<defaultgrade>" + question[i].defaultgrade + "</defaultgrade>\n\t</question>\n";
                if (count > 1 && i != count - 1)
                {
                    allquestions += "\t";
                }
            }
            return allquestions + "</quiz>";

        }
    }
}
