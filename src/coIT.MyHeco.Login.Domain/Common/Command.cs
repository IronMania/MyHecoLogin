using System;

namespace coIT.MyHeco.Login.Domain.Common
{
    public class Command<T>
    {
        private readonly Benutzer _defaultValue;
        private readonly Func<bool> _canRun;
        private readonly Func<T, Benutzer> _run;

        public Command(Benutzer defaultValue)
        {
            _defaultValue = defaultValue;
            _canRun = () => false;
            _run = arg => _defaultValue;
        }

        public Command(Func<T, Benutzer> register)
        {
            _run = register;
            _canRun = () => true;
        }

        public static Command<T> OffCommand(Benutzer defaultValue)
        {
            return new Command<T>(defaultValue);
        }

        public static Command<T> AlwaysOn(Func<T, Benutzer> register)
        {
            return new Command<T>(register);
        }

        public bool CanRun()
        {
            return _canRun();
        }

        public Benutzer Run(T parameter)
        {
            if (CanRun())
                return _run(parameter);
            return _defaultValue;
        }
    }

    public class Command
    {
        private readonly Benutzer _defaultValue;
        private readonly Func<bool> _canRun;
        private readonly Func<Benutzer> _run;

        public Command(Benutzer defaultValue)
        {
            _defaultValue = defaultValue;
            _canRun = () => false;
            _run = () => _defaultValue;
        }

        public Command(Func< Benutzer> register)
        {
            _run = register;
            _canRun = () => true;
        }

        public static Command OffCommand(Benutzer defaultValue)
        {
            return new Command(defaultValue);
        }

        public static Command AlwaysOn(Func<Benutzer> register)
        {
            return new Command(register);
        }

        public bool CanRun()
        {
            return _canRun();
        }

        public Benutzer Run( )
        {
            if (CanRun())
                return _run();
            return _defaultValue;
        }
    }
}