using Microsoft.VisualStudio.TestTools.UnitTesting;
using TRPO_Lab8;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;

namespace Lab_8_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        /// Проверяет работу алгоритма поиска записей в документе. Ожидается, что найдется не первая попавшаяся запись, а все 11.
        public void Selection_Name_11_expexted()
        {
            int counter = 0;
            int expected = 11;
            string keyword = "true";
            IEnumerable<XElement> elements = null;
            string filename = "Subject.xml";
            XDocument doc = XDocument.Load(filename);
            elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("has_coursework") == keyword).ToList();
            foreach (XElement s in elements)
            {
                counter++;
            }
            Assert.AreEqual(expected, counter);
        }
        [TestMethod]
        public void Selection_Name_1_expexted()
        {
            int counter = 0;
            int expected = 1;
            string keyword = "Ivanov";
            IEnumerable<XElement> elements = null;
            string filename = "Subject.xml";
            XDocument doc = XDocument.Load(filename);
            elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("teacher_lastname") == keyword).ToList();
            foreach (XElement s in elements)
            {
                counter++;
            }
            Assert.AreEqual(expected, counter);
        }
        [TestMethod]
        /// Проверяет работу алгоритма поиска записей в документе. Ожидается, что записей не найдется.

        public void Selection_Name_0_expexted()
        {
            int counter = 0;
            int expected = 0;
            string keyword = "BBBUUUSSS";
            IEnumerable<XElement> elements = null;
            string filename = "Subject.xml";
            XDocument doc = XDocument.Load(filename);
            elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("teacher_lastname") == keyword).ToList();
            foreach (XElement s in elements)
            {
                counter++;
            }
            Assert.AreEqual(expected, counter);
        }
        [TestMethod]
        /// Проверяет работу алгоритма удаления записей в документе (когда введены все поля). Ожидается, что удалится  1 запись.

        public void Deleting_1_expexted()
        {
            int counter = 0;
            int expected = 1;
            string text1 = "1";
            string text2 = "Maths";
            string text3 = "Ivanov";
            string text4 = "3530903/70302";
            string text5 = "30";
            string text6 = "50";
            string text7 = "80";
            string text8 = "true";
            string text9 = "exam";
            IEnumerable<XElement> elements = null;
            string filename = "Subject.xml";
            XDocument doc = XDocument.Load(filename);
            elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("subject_id") == text1
            && (string)s.Element("group_id").Value == text4
            && (string)s.Element("subject_name").Value == text2
            && (string)s.Element("teacher_lastname").Value == text3
            && (string)s.Element("group_quantity").Value == text5
            && (string)s.Element("lection_hours").Value == text6
            && (string)s.Element("practice_hours").Value == text7
            && (string)s.Element("has_coursework").Value == text8
            && (string)s.Element("final_control").Value == text9
            ).ToList();
            foreach (XElement s in elements)
            {
                s.Remove();
                counter++;
            }
            Assert.AreEqual(expected, counter);
            //Selection_Name_1_expexted();
        }
        [TestMethod]
        /// Проверяет работу алгоритма удаления записей в документе (когда введены все поля). Ожидается, что ничего не удалится.

        public void Deleting_0_expexted()
        {
            int counter = 0;
            int expected = 0;
            string text1 = "1";
            string text2 = "Maths";
            string text3 = "Ivanov";
            string text4 = "3530903/70302";
            string text5 = "30";
            string text6 = "50";
            string text7 = "80";
            string text8 = "true";
            string text9 = "exam";
            IEnumerable<XElement> elements = null;
            string filename = "Subject.xml";
            XDocument doc = XDocument.Load(filename);
            elements = doc.Root.Descendants("subject")
            .Where(s => (string)s.Element("subject_id") == text1
            && (string)s.Element("group_id").Value == text2
            && (string)s.Element("subject_name").Value == text3
            && (string)s.Element("teacher_lastname").Value == text4
            && (string)s.Element("group_quantity").Value == text5
            && (string)s.Element("lection_hours").Value == text6
            && (string)s.Element("practice_hours").Value == text7
            && (string)s.Element("has_coursework").Value == text8
            && (string)s.Element("final_control").Value == text9
            ).ToList();
            foreach (XElement s in elements)
            {
                s.Remove();
                counter++;
            }
            Assert.AreEqual(expected, counter);
        }
        [TestMethod]
        /// проверяется алгоритм добавления записей в файл. В тесте добавляется уже существующая запись и ожидается, что после добавления
        /// алгоритм поиска выведет две записи.
        public void Adding_2_expexted()
        {
            int counter = 0;
            string text1 = "1";
            string text2 = "Maths";
            string text3 = "Ivanov";
            string text4 = "3530903/70302";
            string text5 = "30";
            string text6 = "50";
            string text7 = "80";
            string text8 = "true";
            string text9 = "exam";
            string filename = "Subject.xml";
            XDocument doc = XDocument.Load(filename);
            XElement subject = new XElement("subject",
                       new XElement("subject_id", text1),
                       new XElement("subject_name", text2),
                       new XElement("teacher_lastname", text3),
                       new XElement("group_id", text4),
                       new XElement("group_quantity", text5),
                       new XElement("lection_hours", text6),
                       new XElement("practice_hours", text7),
                       new XElement("has_coursework", text8),
                       new XElement("final_control", text9));
            doc.Root.Add(subject);
            int expected = 2;
            string keyword = "Ivanov";
            IEnumerable<XElement> elements = null;
            elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("teacher_lastname") == keyword).ToList();
            foreach (XElement s in elements)
            {
                counter++;
            }
            Assert.AreEqual(expected, counter);
        }
        [TestMethod]
        /// проверяется алгоритм поиска при введении неверного имени поля элемента. Ожидается, что не найдется ни одной записи
        public void Selection_Wrong_elementName_0_expected()
        {
            int counter = 0;
            int expected = 0;
            string keyword = "Ivanov";
            IEnumerable<XElement> elements = null;
            string filename = "Subject.xml";
            XDocument doc = XDocument.Load(filename);
            elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("teacher_lastnameee") == keyword).ToList();
            foreach (XElement s in elements)
            {
                counter++;
            }
            Assert.AreEqual(expected, counter);
        }
        [TestMethod]
        /// проверяется алгоритм поиска при введении неверного имени элемента. Ожидается, что не найдется ни одной записи

        public void Selection_Wrong_Descendances_0_expected()
        {
            int counter = 0;
            int expected = 0;
            string keyword = "Ivanov";
            IEnumerable<XElement> elements = null;
            string filename = "Subject.xml";
            XDocument doc = XDocument.Load(filename);
            elements = doc.Root.Descendants("subjectttt").Where(s => (string)s.Element("teacher_lastnameee") == keyword).ToList();
            foreach (XElement s in elements)
            {
                counter++;
            }
            Assert.AreEqual(expected, counter);
        }
        [TestMethod]
        /// проверяется алгоритм удаления (когда введено одно поле). Ожидается, алгоритм удалит не первую попавшуюся запись, а все 11.

        public void MulitpleDeleting_11_expected()
        {
            int counter = 0;
            int expected = 11;
            string keyword = "true";
            IEnumerable<XElement> elements = null;
            string filename = "Subject.xml";
            XDocument doc = XDocument.Load(filename);
            elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("has_coursework") == keyword).ToList();
            foreach (XElement s in elements)
            {
                counter++;
                s.Remove();
            }
            Assert.AreEqual(expected, counter);
        }
    }
}
