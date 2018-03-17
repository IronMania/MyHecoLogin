using System;

namespace coIT.MyHeco.Core
{
    public class Command<T, T1>
    {
        private readonly Func<bool> _canRun;
        private readonly T _defaultValue;
        private readonly Func<T1, T> _run;

        public Command(T defaultValue)
        {
            _defaultValue = defaultValue;
            _canRun = () => false;
            _run = arg => _defaultValue;
        }

        public Command(Func<T1, T> register)
        {
            _run = register;
            _canRun = () => true;
        }

        public static Command<T, T1> OffCommand(T defaultValue)
        {
            return new Command<T, T1>(defaultValue);
        }

        public static Command<T, T1> AlwaysOn(Func<T1, T> register)
        {
            return new Command<T, T1>(register);
        }

        public bool CanRun()
        {
            return _canRun();
        }

        public T Run(T1 parameter)
        {
            if (CanRun())
                return _run(parameter);
            return _defaultValue;
        }
    }

    public class Command<T>
    {
        private readonly Func<bool> _canRun;
        private readonly T _defaultValue;
        private readonly Func<T> _run;

        public Command(T defaultValue)
        {
            _defaultValue = defaultValue;
            _canRun = () => false;
            _run = () => _defaultValue;
        }

        public Command(Func<T> register)
        {
            _run = register;
            _canRun = () => true;
        }

        public static Command<T> OffCommand(T defaultValue)
        {
            return new Command<T>(defaultValue);
        }

        public static Command<T> AlwaysOn(Func<T> register)
        {
            return new Command<T>(register);
        }

        public bool CanRun()
        {
            return _canRun();
        }

        public T Run()
        {
            if (CanRun())
                return _run();
            return _defaultValue;
        }
    }
}