﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_7_2.Models.Wrappers
{
    public class StatusWrapper : IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Id):
                        if (Id == 0)
                            Error = "Pole \"Status\" jest wymagane!";
                        else
                            Error = string.Empty;
                        break;
                    default:
                        break;
                }

                return Error;
            }
        }

        public bool IsValid
        {
            get
            {
                return string.IsNullOrWhiteSpace(Error);
            }
        }

    }
}
