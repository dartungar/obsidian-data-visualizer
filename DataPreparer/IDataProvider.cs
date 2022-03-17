using Common;

namespace DataProcessor
{
    public interface IDataProvider
    {
        public void ReadData();
        public IEnumerable<DataPoint> GetData();

    }
}
