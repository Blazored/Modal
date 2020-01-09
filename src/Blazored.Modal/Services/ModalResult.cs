using System;

namespace Blazored.Modal.Services
{
    public class ModalResult
    {
        public object Data { get; }
        public Type DataType { get; }
        public bool Cancelled { get; }
        public Type ModalType { get; set; }

        protected ModalResult(object data, Type resultType, bool cancelled, Type modalType)
        {
            Data = data;
            DataType = resultType;
            Cancelled = cancelled;
            ModalType = modalType;
        }

        public static ModalResult Ok<T>(T result) => Ok(result, default);

        public static ModalResult Ok<T>(T result, Type modalType) => new ModalResult(result, typeof(T), false, modalType);

        public static ModalResult Cancel() => Cancel(default);

        public static ModalResult Cancel(Type modelType) => new ModalResult(default, typeof(object), true, modelType);

    }
}
