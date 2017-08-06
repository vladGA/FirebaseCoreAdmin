namespace FirebaseCoreAdmin.Configurations.AuthPayload
{
    using System.Collections.Generic;

    public abstract class PayloadGenerator
    {
        private Dictionary<string, string> _payload = new Dictionary<string, string>();

        protected void AddToPayload(string key, string value)
        {
            if (_payload.ContainsKey(key))
                _payload[key] = value;
            else
                _payload.Add(key, value);
        }

        protected IDictionary<string, string> GetPayloadData()
        {
            return _payload;
        }

        public virtual IDictionary<string, string> GetPayload(IDictionary<string, string> additionalPayload = null)
        {
            if (additionalPayload == null)
                return GetPayloadData();

            foreach (var item in additionalPayload)
                AddToPayload(item.Key, item.Value);

            return GetPayloadData();
        }
    }
}
