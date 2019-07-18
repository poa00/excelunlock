﻿using Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Data
{
    /// <summary>
    /// Запись об одном человеке
    /// </summary>
    public class Record
    {
        private Dictionary<string, string> OneStudent;
        public Dictionary<string, string> GetOneStudent() => OneStudent;

        public Record()
        {
            OneStudent = new Dictionary<string, string>();
        }
        
        /// <summary>
        /// Добавить новую запись
        /// Если ее нет вставить пробел
        /// </summary>
        /// <param name="bookmark">Название записи</param>
        /// <param name="value">Запись</param>
        public void AddPropertyRecord(string bookmark, string value)
        {
            if (bookmark == null || value == null)
                return;
            OneStudent.Add(bookmark.Trim(), Check(bookmark, value));
        }

        /// <summary>
        /// Удаляет запись
        /// </summary>
        /// <param name="bookmark">Назваание записи</param>
        public void RemoveRecord(string bookmark)
        {
            if (bookmark == null)
                return;
            OneStudent.Remove(bookmark);
        }

        /// <summary>
        /// Обединение записей
        /// </summary>
        /// <param name="record">Массив записей</param>
        public void ConcatRecord(Record record)
        {
            if (record == null)
            {
                throw new Exception("Запись равна null");
            }

            OneStudent = OneStudent.Concat(record.OneStudent.Where(kvp => !OneStudent.ContainsKey(kvp.Key)))
                                                .OrderBy(c => c.Value)
                                                .ToDictionary(c => c.Key, c => c.Value);           
        }
        /// <summary>
        /// Проверяет данные
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">значение</param>
        /// <returns></returns>
        private string Check(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                value = " ";
                return value;
            }

            if (key.Contains("Дата") || key.Contains("дата"))
            {
                return CheckDate(value.Trim());
            }
            if (key.Contains("Оценка") || key.Contains("оценка"))
            {
                return CheckMark(value.Trim());
            }

            return value.Trim();
        }

        /// <summary>
        /// Поверяет наличие данных (даты)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string CheckDate(string date)
        {
            if (date.Length > 10)
            {
                return date.Substring(0, 10);
            }
            if (date.Length < 10)
            {
                MessageBug.AddMessage("Даты нет");
                date = "01.01.0001";
            }
            return date;
        }

        /// <summary>
        /// Поверяет наличие данных (Оценки)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string CheckMark(string date)
        {
            if (date.Length > 1)
            {
                if (date == "пять" || date == "четыре" || date == "три")
                {
                    
                }
                else
                {
                    MessageBug.AddMessage("Оценку запишите цифрой (5)");
                    date = " ";
                    return date;
                }
            }
            return date;
        }

    }
}
