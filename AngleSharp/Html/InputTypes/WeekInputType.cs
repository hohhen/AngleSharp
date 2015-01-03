﻿namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class WeekInputType : BaseInputType
    {
        #region ctor

        public WeekInputType(String name)
            : base(name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value;
            var date = ConvertFromWeek(value);

            if (date.HasValue)
            {
                var step = GetStep(input);
                var min = ConvertFromWeek(input.Minimum);
                var max = ConvertFromWeek(input.Maximum);

                state.IsRangeUnderflow = min.HasValue && date < min.Value;
                state.IsRangeOverflow = max.HasValue && date > max.Value;
                state.IsValueMissing = false;
                state.IsBadInput = false;
                state.IsStepMismatch = step != 0.0 && GetStepBase(input) % step != 0.0;
            }
            else
            {
                state.IsRangeUnderflow = false;
                state.IsRangeOverflow = false;
                state.IsStepMismatch = false;
                state.IsValueMissing = input.IsRequired;
                state.IsBadInput = !String.IsNullOrEmpty(value);
            }
        }

        public override Double? ConvertToNumber(String value)
        {
            var dt = ConvertFromWeek(value);

            if (dt.HasValue)
                return dt.Value.Subtract(new DateTime(1970, 1, 5, 0, 0, 0)).TotalMilliseconds;

            return null;
        }

        public override DateTime? ConvertToDate(String value)
        {
            return ConvertFromWeek(value);
        }

        public override void DoStep(IHtmlInputElement input, Int32 n)
        {
            var dt = ConvertFromWeek(input.Value);

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep(input) * n);
                var min = ConvertFromWeek(input.Minimum);
                var max = ConvertFromWeek(input.Maximum);

                if ((min.HasValue == false || min.Value <= date) && (max.HasValue == false || max.Value >= date))
                    input.ValueAsDate = date;
            }
        }

        #endregion

        #region Step

        protected override Double GetDefaultStepBase(IHtmlInputElement input)
        {
            return 0.0;
        }

        protected override Double GetDefaultStep(IHtmlInputElement input)
        {
            return 1.0;
        }

        protected override Double GetStepScaleFactor(IHtmlInputElement input)
        {
            return 604800000.0;
        }

        #endregion
    }
}
