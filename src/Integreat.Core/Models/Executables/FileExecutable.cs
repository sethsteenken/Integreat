using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Integreat.Plugins;

namespace Integreat.Core
{
    public abstract class FileExecutable : ExecutableBase
    {
        //private readonly IFileStorage _fileStorage;

        //protected FileExecutable(IProcess process, IFileStorage fileStorage, ExecutableReference reference)
        //    : this(process, fileStorage, reference.Timeout, reference.Parameters, reference.File)
        //{

        //}

        //protected FileExecutable(IProcess process, IFileStorage fileStorage, int timeout, ExecutionParameters parameters, string file)
        //    : base(process, timeout, parameters)
        //{
        //    if (string.IsNullOrWhiteSpace(file))
        //        throw new ArgumentNullException(nameof(file), $"A filename value for File is required to execute {GetType().FullName} executable.");

        //    _fileStorage = fileStorage;
        //    File = file.Trim();
        //}

        protected string File { get; private set; }
        //protected IFileStorage FileStorage => _fileStorage;

        public override string Name => $"Type: {GetType().Name}, File: {File}";

        protected override void OnExecute(ExecutableContext context)
        {
            string filePath = "";// _fileStorage.Join(executablesDirectory, File);

            //if (!_fileStorage.Exists(filePath))
            //    throw new FileNotFoundException($"Executable file '{filePath}' not found.");

            OnExecute(context, filePath);
        }

        protected abstract void OnExecute(ExecutableContext context, string filePath);
    }
}
