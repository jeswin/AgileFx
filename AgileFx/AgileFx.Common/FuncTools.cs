/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AgileFx
{
    public class F
    {
        //Y-Combinator (Recursive Lambdas)
        public delegate Action<A> Recursive<A>(Recursive<A> r);
        public delegate Func<A, R> Recursive<A, R>(Recursive<A, R> r);
        public delegate Func<A, B, R> Recursive<A, B, R>(Recursive<A, B, R> r);
        public delegate Func<A, B, C, R> Recursive<A, B, C, R>(Recursive<A, B, C, R> r);
        public delegate Func<A, B, C, D, R> Recursive<A, B, C, D, R>(Recursive<A, B, C, D, R> r);

        public static Action<A> Y<A>(Func<Action<A>, Action<A>> f)
        {
            Recursive<A> rec = r => a => f(r(r))(a);
            return rec(rec);
        }

        public static Func<A, R> Y<A, R>(Func<Func<A, R>, Func<A, R>> f)
        {
            Recursive<A, R> rec = r => a => f(r(r))(a);
            return rec(rec);
        }

        public static Func<A, B, R> Y<A, B, R>(Func<Func<A, B, R>, Func<A, B, R>> f)
        {
            Recursive<A, B, R> rec = r => (a, b) => f(r(r))(a, b);
            return rec(rec);
        }

        public static Func<A, B, C, R> Y<A, B, C, R>(Func<Func<A, B, C, R>, Func<A, B, C, R>> f)
        {
            Recursive<A, B, C, R> rec = r => (a, b, c) => f(r(r))(a, b, c);
            return rec(rec);
        }

        public static Func<A, B, C, D, R> Y<A, B, C, D, R>(Func<Func<A, B, C, D, R>, Func<A, B, C, D, R>> f)
        {
            Recursive<A, B, C, D, R> rec = r => (a, b, c, d) => f(r(r))(a, b, c, d);
            return rec(rec);
        }
    }
}
