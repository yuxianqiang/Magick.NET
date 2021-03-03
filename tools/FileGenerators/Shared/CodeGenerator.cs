﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.CodeDom.Compiler;
using System.IO;

namespace FileGenerator
{
    public abstract class CodeGenerator
    {
        private IndentedTextWriter _Writer;

        protected CodeGenerator()
        {
        }

        protected CodeGenerator(CodeGenerator parent)
        {
            _Writer = parent._Writer;
        }

        protected int Indent
        {
            get
            {
                return _Writer.Indent;
            }
            set
            {
                _Writer.Indent = value;
            }
        }

        protected void Write(char value)
        {
            _Writer.Write(value);
        }

        protected void Write(int value)
        {
            _Writer.Write(value);
        }

        protected void Write(string value)
        {
            _Writer.Write(value);
        }

        protected void WriteEnd()
        {
            WriteEndColon();
        }

        protected void WriteElse(string action)
        {
            WriteLine("else");
            Indent++;
            WriteLine(action);
            Indent--;
        }

        protected void WriteEndColon()
        {
            Indent--;
            WriteLine("}");
        }

        protected void WriteIf(string condition, string action)
        {
            WriteLine("if (" + condition + ")");
            Indent++;
            WriteLine(action);
            Indent--;
        }

        protected void WriteLine()
        {
            int indent = Indent;
            Indent = 0;
            _Writer.WriteLine();
            Indent = indent;
        }

        protected void WriteLine(string value)
        {
            _Writer.WriteLine(value);
        }

        protected void WriteQuantumType()
        {
            _Writer.WriteLine();
            _Writer.WriteLine("#if Q8");
            _Writer.WriteLine("using QuantumType = System.Byte;");
            _Writer.WriteLine("#elif Q16");
            _Writer.WriteLine("using QuantumType = System.UInt16;");
            _Writer.WriteLine("#elif Q16HDRI");
            _Writer.WriteLine("using QuantumType = System.Single;");
            _Writer.WriteLine("#else");
            _Writer.WriteLine("#error Not implemented!");
            _Writer.WriteLine("#endif");
            _Writer.WriteLine();
        }

        protected void WriteStart(string namespaceName)
        {
            _Writer.WriteLine("// Copyright 2013-" + DateTime.Now.Year + " Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>");
            _Writer.WriteLine("//");
            _Writer.WriteLine("// Licensed under the ImageMagick License (the \"License\"); you may not use this file except in");
            _Writer.WriteLine("// compliance with the License. You may obtain a copy of the License at");
            _Writer.WriteLine("//");
            _Writer.WriteLine("//   https://imagemagick.org/script/license.php");
            _Writer.WriteLine("//");
            _Writer.WriteLine("// Unless required by applicable law or agreed to in writing, software distributed under the");
            _Writer.WriteLine("// License is distributed on an \"AS IS\" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,");
            _Writer.WriteLine("// either express or implied. See the License for the specific language governing permissions");
            _Writer.WriteLine("// and limitations under the License.");
            _Writer.WriteLine("// <auto-generated/>");
            _Writer.WriteLine();
            WriteUsing();
            _Writer.WriteLine("namespace " + namespaceName);
            WriteStartColon();
        }

        protected void WriteStartColon()
        {
            WriteLine("{");
            Indent++;
        }

        protected virtual void WriteUsing()
        {
        }

        public void CloseWriter()
        {
            _Writer.InnerWriter.Dispose();
            _Writer.Dispose();
        }

        public void CreateWriter(string fileName)
        {
            Console.WriteLine("Creating: " + fileName);

            StreamWriter streamWriter = new StreamWriter(fileName);
            _Writer = new IndentedTextWriter(streamWriter, "    ");
        }
    }
}
