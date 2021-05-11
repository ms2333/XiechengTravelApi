using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XiechengTravel.ResourceParameters
{
    /// <summary>
    /// 对资源过滤器进行封装
    /// </summary>
    public class TouristRouteResourceParamaters
    {
        public int TotalPages { get; private set; }//总页
        public int TotalCount { get; private set; }//总数量

        public string Keyword { get; set; }
        public string RatingOperatorType { get; set; }
        public int RatingValue { get; set; } = -1;

        private int _currentPage;
        public int CurrentPage {
            get {
                return _currentPage;
            } set
            {
                if (value <= 1)
                {
                    this._currentPage = 1;
                }
                else
                {
                    this._currentPage = value;
                }
            }
        }

        private int _pageSize;
        public int PageSize  {
            get {
                return _pageSize;
            } set
            { 
                if(value<=0)
                {
                    this._pageSize = 10;
                }
                else
                {
                    this._pageSize = value;
                }
                
            }
        }

        private string _rating;
        public string Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                if (value != null)
                {
                    Regex regex = new Regex(@"([A-Za-z0-9\-]+)(\d+)");
                    Match match = regex.Match(value);
                    if (match.Success)
                    {
                        RatingOperatorType = match.Groups[1].Value;
                        RatingValue = int.Parse(match.Groups[2].Value);
                    }
                }
                _rating = value;

            }
        }//lessThan,largerThan,equalTo ====>lessThan3,largerThan4,equalTo1
    }
}
