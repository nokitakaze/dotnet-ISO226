using System;
using System.Collections.Generic;

namespace ISO226
{
    public static class EqualLoudnessContour
    {
        public static readonly IReadOnlyList<double> iso_f = new[]
        {
            20, 25, 31.5, 40, 50, 63, 80, 100, 125, 160, 200, 250, 315, 400, 500, 630, 800, 1000, 1250, 1600, 2000,
            2500, 3150, 4000, 5000, 6300, 8000, 10000, 12500
        };

        public static readonly IReadOnlyList<double> iso_a_f = new[]
        {
            0.532, 0.506, 0.480, 0.455, 0.432, 0.409, 0.387, 0.367, 0.349, 0.330, 0.315, 0.301, 0.288, 0.276, 0.267,
            0.259, 0.253, 0.250, 0.246, 0.244, 0.243, 0.243, 0.243, 0.242, 0.242, 0.245, 0.254, 0.271, 0.301
        };

        public static readonly IReadOnlyList<double> iso_L_u = new[]
        {
            -31.6, -27.2, -23.0, -19.1, -15.9, -13.0, -10.3, -8.1, -6.2, -4.5, -3.1, -2.0, -1.1, -0.4, 0.0, 0.3, 0.5,
            0.0, -2.7, -4.1, -1.0, 1.7, 2.5, 1.2, -2.1, -7.1, -11.2, -10.7, -3.1
        };

        public static readonly IReadOnlyList<double> iso_T_f = new[]
        {
            78.5, 68.7, 59.5, 51.1, 44.0, 37.5, 31.5, 26.5, 22.1, 17.9, 14.4, 11.4, 8.6, 6.2, 4.4, 0, 2.2, 2.4, 3.5,
            1.7, -1.3, -4.2, -6.0, -5.4, -1.5, 6.0, 12.6, 13.9, 12.3
        };

        /// <summary>
        /// Показатель экспоненты для ощущения громкости
        /// </summary>
        /// <param name="hz"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static double Get_a_f(double hz)
        {
            for (int i = 0; i < iso_f.Count; i++)
            {
                if (hz == iso_f[i])
                {
                    return iso_a_f[i];
                }
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Модуль передаточной функции линейной системы, нормированный по частоте 1000 Гц
        /// </summary>
        /// <param name="hz"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static double Get_L_u(double hz)
        {
            for (int i = 0; i < iso_f.Count; i++)
            {
                if (hz == iso_f[i])
                {
                    return iso_L_u[i];
                }
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Порог слышимости
        /// </summary>
        /// <param name="hz"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static double Get_T_f(double hz)
        {
            for (int i = 0; i < iso_f.Count; i++)
            {
                if (hz == iso_f[i])
                {
                    return iso_T_f[i];
                }
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Уровень громкости (в фон) превратить в уровень звукового давления (в db)
        /// </summary>
        /// <param name="phon"></param>
        /// <param name="hz"></param>
        /// <returns></returns>
        public static double ConvertPhonToDb(double phon, double hz)
        {
            // Модуль передаточной функции линейной системы
            var L_u = Get_L_u(hz);
            // Показатель экспоненты для ощущения громкости
            var a_f = Get_a_f(hz);

            double A_t;
            {
                var L_n = phon;
                // Порог слышимости
                var T_f = Get_T_f(hz);

                var v1 = Math.Pow(10, 0.025 * L_n) - 1.15;
                var v2a = 0.4 * Math.Pow(10, 0.1 * (T_f + L_u) - 9);
                var v2 = Math.Pow(v2a, a_f);

                A_t = 4.47e-3 * v1 + v2;
            }

            // Уровень звукового давления
            var L_p = 10d / a_f * Math.Log10(A_t) - L_u + 94;

            return L_p;
        }

        public static double ConvertDbToPhon(double db, double hz)
        {
            var L_p = db;

            // Модуль передаточной функции линейной системы
            var L_u = Get_L_u(hz);
            // Порог слышимости
            var T_f = Get_T_f(hz);
            // Показатель экспоненты для ощущения громкости
            var a_f = Get_a_f(hz);

            var v1a = (L_p + L_u) * 0.1d - 9;
            var v1 = 0.4 * Math.Pow(10, v1a);
            var v2a = (T_f + L_u) * 0.1d - 9;
            var v2 = 0.4 * Math.Pow(10, v2a);

            var B_t = Math.Pow(v1, a_f) - Math.Pow(v2, a_f) + 0.005135d;

            var L_n = 40 * Math.Log10(B_t) + 94;

            return L_n;
        }
    }
}