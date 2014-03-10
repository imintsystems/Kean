// 
//  Double.cs (generated by template)
//  
//  Author:
//       Anders Frisk <andersfrisk77@gmail.com>
//  
//  Copyright (c) 2011 Anders Frisk
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using NUnit.Framework;
using Kean.Extension;
using Target = Kean.Math.Matrix.Double;

namespace Kean.Math.Matrix.Test
{
    public class Double :
        Kean.Test.Fixture<Double>
    {
        protected override void Run()
        {
            this.Run(
                this.Constructor1,
                this.Constructor2,
                this.Equality,
                this.Adjoint,
                this.All,
                this.Basis,
                this.Block,
                this.Copy,
                this.Diagonal,
                this.Platformct,
                this.Identity,
                this.KroneckerProduct,
                this.Minor,
                this.Paste,
                this.Set,
                this.Transpose,
                this.Cholesky1,
                this.Cholesky2,
                this.DeterminantAndTrace,
                this.Inverse1,
                this.Inverse2,
                this.LeastSquare1,
                this.LeastSquare2,
                this.LeastSquare3
                );
        }
        string prefix = "Kean.Math.Matrix.Test.Double.";
        #region Constructors
        [Test]
        public void Constructor1()
        {
            Target a = new Target(3, 5);
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 5; y++)
                    Verify(a[x, y], Is.EqualTo(0), this.prefix + "Constructor1.0");
            Verify(a.Dimensions.Width, Is.EqualTo(3), this.prefix + "Constructor1.1");
            Verify(a.Dimensions.Height, Is.EqualTo(5), this.prefix + "Constructor1.2");
            Verify(a.Order, Is.EqualTo(3), this.prefix + "Constructor1.3");
            Verify(a.IsSquare, Is.False, this.prefix + "Constructor1.4");
        }
        [Test]
        public void Constructor2()
        {
            double[] values = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            Target a = new Target(3, 5, values);
            int k = 1;
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 5; y++)
                    Verify(a[x, y], Is.EqualTo(k++), this.prefix + "Constructor2.0");
        }
        #endregion
        #region Equality
        [Test]
        public void Equality()
        {
            double[] values = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            Target a = new Target(3, 5, values);
            double[] values2 = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            Target b = new Target(3, 5, values2);
            Verify(a, Is.EqualTo(b), this.prefix + "Equality.0");
            b[2, 2] = -1;
            Verify(a, Is.Not.EqualTo(b), this.prefix + "Equality.1");
            double[] values3 = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            Target c = new Target(1, 5, values3);
            Verify(a, Is.Not.EqualTo(c), this.prefix + "Equality.2");
        }
        #endregion
        #region Static Constructors
        [Test]
        public void Identity()
        {
            Target a = Target.Identity(5);
            for (int x = 0; x < 5; x++)
                for (int y = 0; y < 5; y++)
                    Verify(a[x, y], Is.EqualTo(x == y ? 1 : 0));
            Verify(a.IsDiagonal(0), Is.True, this.prefix + "Identity.0");
        }
        [Test]
        public void Basis()
        {
            Target a = Target.Basis(10, 2);
            for (int y = 0; y < 10; y++)
                Verify(a[0, y], Is.EqualTo(y == 2 ? 1 : 0), this.prefix + "Basis.0");
        }
        [Test]
        public void Diagonal()
        {
            Target a = new Target(2, 2, new double[] { 1, 2, 3, 4 });
            Target b = new Target(3, 3, new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Target c = Target.Diagonal(a, b);
            Target d = new Target(5, 5, new double[] { 1, 2, 0, 0, 0, 3, 4, 0, 0, 0, 0, 0, 1, 2, 3, 0, 0, 4, 5, 6, 0, 0, 7, 8, 9 });
            Verify(c, Is.EqualTo(d), this.prefix + "Diagonal.0");
        }
        [Test]
        public void Block()
        {
            Target a00 = new Target(2, 2, new double[] { 1, 2, 3, 4 });
            Target a01 = new Target(2, 3, new double[] { 1, 2, 3, 4 });
            Target a11 = new Target(3, 3, new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Target a10 = new Target(3, 2, new double[] { 1, 2, 3, 4 });
            Target[,] blocks = new Target[2, 2];
            blocks[0, 0] = a00;
            blocks[0, 1] = a01;
            blocks[1, 0] = a10;
            blocks[1, 1] = a11;
            Target c = Target.Block(blocks);
            Target d = new Target(5, 5);
            d.Set(0, 0, a00);
            d.Set(0, 2, a01);
            d.Set(2, 0, a10);
            d.Set(2, 2, a11);
            Verify(c, Is.EqualTo(d), this.prefix + "Block.0");
        }
        #endregion
        #region Artithmetics
        [Test]
        public void All()
        {
            Target a = new Target(2, 3, new double[] { 1, 2, 3, 4, 5, 6 });
            Target b = new Target(2, 3, new double[] { 2, 4, 6, 8, 10, 12 });
            Target c = new Target(2, 3, new double[] { -1, -2, -3, -4, -5, -6 });
            Target d = new Target(2, 3);
            Verify(a + a, Is.EqualTo(b), this.prefix + "All.0");
            Verify(-a, Is.EqualTo(c), this.prefix + "All.1");
            Verify(a - a, Is.EqualTo(d), this.prefix + "All.2");
            Verify(a * -1, Is.EqualTo(c), this.prefix + "All.3");
            Verify((-1) * a, Is.EqualTo(c), this.prefix + "All.4");
            Verify(a / -1, Is.EqualTo(c), this.prefix + "All.5");
        }
        [Test]
        public void MatrixMatrixMultiplication()
        {
            Target a = new Target(2, 3, new double[] { 1, 2, 3, 4, 5, 6 });
            Target b = new Target(2, 2, new double[] { 1, 2, 3, 4 });
            Target c = new Target(2, 3, new double[] { 9, 12, 15, 19, 26, 33 });
            Verify(a * b, Is.EqualTo(c), this.prefix + "MatrixMatrixMultiplication.0");
        }
        [Test]
        public void KroneckerProduct()
        {
            Target a = new Target(2, 3, new double[] { 1, 2, 3, 4, 5, 6 });
            Target b = new Target(2, 2, new double[] { 1, 2, 3, 4 });
            Target c = new Target(4, 6, new double[] { 1, 2, 2, 4, 3, 6, 3, 4, 6, 8, 9, 12, 4, 8, 5, 10, 6, 12, 12, 16, 15, 20, 18, 24 });
            Verify(a.Kronecker(b), Is.EqualTo(c), this.prefix + "KroneckerProduct.0");
        }
        #endregion
        #region Matrix operator
        [Test]
        public void Copy()
        {
            double[] values = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            Target a = new Target(3, 5, values);
            Verify(a, Is.EqualTo(a.Copy()), this.prefix + "Copy.0");
        }
        [Test]
        public void Adjoint()
        {
            Target a = new Target(3, 3, new double[] { -3, -1, 3, 2, 0, -4, -5, -2, 1 });
            Target b = new Target(3, 3, new double[] { -8, -5, 4, 18, 12, -6, -4, -1, 2 });
            Verify((a.Adjoint() - b).Norm, Is.LessThan(1e-6), this.prefix + "Adjoint.0");
        }
        [Test]
        public void Transpose()
        {
            Target a = new Target(2, 3, new double[] { 1, 2, 3, 4, 5, 6 });
            Target b = new Target(3, 2, new double[] { 1, 4, 2, 5, 3, 6 });
            Verify(a.Transpose(), Is.EqualTo(b), this.prefix + "Transpose.0");
        }
        [Test]
        public void Minor()
        {
            Target a = new Target(2, 3, new double[] { 1, 2, 3, 4, 5, 6 });
            Target b = new Target(1, 2, new double[] { 5, 6 });
            Verify(a.Minor(0, 0), Is.EqualTo(b), this.prefix + "Minor.0");
        }
        [Test]
        public void Platformct()
        {
            Target a = new Target(2, 3, new double[] { 1, 2, 3, 4, 5, 6 });
            Target b = new Target(2, 2, new double[] { 1, 2, 4, 5 });
            Verify(a.Extract(0, 2, 0, 2), Is.EqualTo(b), this.prefix + "Platformct.0");
        }
        [Test]
        public void Paste()
        {
            Target a = new Target(2, 3).Paste(0, 0, new Target(2, 2, new double[] { 1, 2, 4, 5 }));
            Target b = new Target(2, 3, new double[] { 1, 2, 0, 4, 5, 0 });
            Verify(a, Is.EqualTo(b), this.prefix + "Paste.0");
        }
        [Test]
        public void Set()
        {
            Target a = new Target(2, 3);
            a.Set(0, 0, new Target(2, 2, new double[] { 1, 2, 4, 5 }));
            Target b = new Target(2, 3, new double[] { 1, 2, 0, 4, 5, 0 });
            Verify(a, Is.EqualTo(b), this.prefix + "Set.0");
        }
        #endregion
        #region Matrix invariants
        [Test]
        public void DeterminantAndTrace()
        {
            Target a = new Target(1, 1);
            Verify(a.Trace, Is.EqualTo(0).Within(1e-7f), this.prefix + "DeterminantAndTrace.0");
            Verify(a.Determinant, Is.EqualTo(0).Within(1e-7f), this.prefix + "DeterminantAndTrace.1");
            a = new Target(5, 5);
            Verify(a.Determinant, Is.EqualTo(0).Within(1e-7f), this.prefix + "DeterminantAndTrace.2");
            Verify(a.Trace, Is.EqualTo(0).Within(1e-7f), this.prefix + "DeterminantAndTrace.3");
            a = Target.Identity(5);
            Verify(a.Determinant, Is.EqualTo(1).Within(1e-7f), this.prefix + "DeterminantAndTrace.4");
            Verify(a.Trace, Is.EqualTo(5).Within(1e-7f), this.prefix + "DeterminantAndTrace.5");
            a = new Target(5, 5, new double[] { 1, 1, 1, 1, 1, 1, 2, 3, 4, 5, 1, 3, 6, 10, 15, 1, 4, 10, 20, 35, 1, 5, 15, 35, 70 });
            a[2, 2] = 1;
            a[4, 4] = -1;
            Verify(a.Determinant, Is.EqualTo(3250).Within(1e-7f), this.prefix + "DeterminantAndTrace.6");
            Verify(a.Trace, Is.EqualTo(23).Within(1e-7f), this.prefix + "DeterminantAndTrace.7");
        }
        #endregion
        #region Matrix inverse
        [Test]
        public void Inverse1()
        {
            Target a = new Target(5, 5);
            Target b = a.Inverse();
            Verify(b.IsNull(), Is.True, this.prefix + "Inverse1.0");
        }
        [Test]
        public void Inverse2()
        {
            Target a = new Target(5, 5, new double[] { 1, 1, 1, 1, 1, 1, 2, 3, 4, 5, 1, 3, 6, 10, 15, 1, 4, 10, 20, 35, 1, 5, 15, 35, 70 });
            Target b = a.Inverse();
            Target correct = new Kean.Math.Matrix.Double(5, 5, new double[] { 5, -10, 10, -5, 1, -10, 30, -35, 19, -4, 10, -35, 46, -27, 6, -5, 19, -27, 17, -4, 1, -4, 6, -4, 1 });
            Verify(b.Distance(correct), Is.EqualTo(0).Within(1e-7f), this.prefix + "Inverse2.0");
        }
        #endregion
        #region Least Square Solvers
        // Standard 
        // Square matrix( full rank)
        [Test]
        public void LeastSquare1()
        {
            Target a = new Target(5, 5, new double[] { 1, 1, 1, 1, 1, 1, 2, 3, 4, 5, 1, 3, 6, 10, 15, 1, 4, 10, 20, 35, 1, 5, 15, 35, 70 });
            Target y = new Target(1, 5, new double[] { -1, 2, -3, 4, 5 });
            Target correct = new Target(1, 5, new double[] { -70, 231, -296, 172, -38 });
            Target x = a.Solve(y);
            Verify(x.Distance(correct), Is.EqualTo(0).Within(1e-7f), this.prefix + "LeastSquare1.0");
        }
        // Overdetermined. Least square solution
        [Test]
        public void LeastSquare2()
        {
            Target a = new Target(5, 5, new double[] { 1, 1, 1, 1, 1, 1, 2, 3, 4, 5, 1, 3, 6, 10, 15, 1, 4, 10, 20, 35, 1, 5, 15, 35, 70 });
            int n = 15;
            Target aa = new Target(5, n * 5);
            for (int i = 0; i < n; i++)
                aa = aa.Paste(0, 5 * i, a);
            Target y = new Target(1, 5, new double[] { -1, 2, -3, 4, 5 });
            Target yy = new Target(1, n * 5);
            for (int i = 0; i < n; i++)
                yy = yy.Paste(0, 5 * i, y);
            Target correct = new Target(1, 5, new double[] { -70, 231, -296, 172, -38 });
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Target x = aa.Solve(yy);
            watch.Stop();
            long timeLup = watch.ElapsedMilliseconds;
            Verify(x.Distance(correct), Is.EqualTo(0).Within(6.5f), this.prefix + "LeastSquare2.0");
            //  Console.WriteLine("Time Lup " + timeLup);

        }
        [Test]
        public void LeastSquare3()
        {
            Target aa = new Target(4, 10, new double[] {
            -100.8856f,   19.1472f,  -58.8784f,  -59.1866f,  -59.0029f,  -38.9253f,  -19.4673f,   18.4284f,   -1.3259f,    0.5993f,
             -19.1472f, -100.8856f,   59.1866f,  -58.8784f,   38.9253f,  -59.0029f,  -18.4284f,  -19.4673f,   -0.5993f,   -1.3259f,
               1.0000f,         0,    1.0000f,         0,    1.0000f,         0,    1.0000f,         0,    1.0000f,         0,
                     0,    1.0000f,         0,    1.0000f,         0,    1.0000f,         0,    1.0000f,         0,    1.0000f});
            Target yy = new Target(1, 10, new double[] {
                 -196.6532f,   33.3493f, -101.8415f, -118.7157f, -106.0487f,  -81.0908f,  -36.6124f,   46.3590f,    5.7252f,    8.7193f});
            Target correct = new Target(1, 4, new double[] { 2.0191f, 0.1504f, 7.8515f, 9.1365f });
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Target x = aa.Solve(yy);
            watch.Stop();
            long timeLup = watch.ElapsedMilliseconds;
            Verify(x.Distance(correct), Is.EqualTo(0).Within(7e-4f), this.prefix + "LeastSquare3.0");
            //   Console.WriteLine("Regression sample. Error " + x.Distance(correct) + " Time Lup " + timeLup);

        }
        #endregion
        #region Cholesky Factorization
        [Test]
        public void Cholesky1()
        {
            Target a = new Target(5, 5);
            Target b = a.Cholesky();
            Verify(b.IsNull(), Is.True, this.prefix + "Cholesky1.0");
        }
        [Test]
        public void Cholesky2()
        {
            Target a = new Target(5, 5, new double[] { 1, 1, 1, 1, 1, 1, 2, 3, 4, 5, 1, 3, 6, 10, 15, 1, 4, 10, 20, 35, 1, 5, 15, 35, 70 });
            Target c = a.Cholesky();
            Verify(c * c.Transpose(), Is.EqualTo(a), this.prefix + "Cholesky2.0");
            Target d = c.Transpose();
            Verify(d.Transpose() * d, Is.EqualTo(a), this.prefix + "Cholesky2.1");
        }
        #endregion
    }
}
