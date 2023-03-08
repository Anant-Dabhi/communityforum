﻿// Lucene version compatibility level 4.8.1
/*

Copyright (c) 2001, Dr Martin Porter
Copyright (c) 2002, Richard Boulton
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice,
    * this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
    * notice, this list of conditions and the following disclaimer in the
    * documentation and/or other materials provided with the distribution.
    * Neither the name of the copyright holders nor the names of its contributors
    * may be used to endorse or promote products derived from this software
    * without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

 */

namespace YAF.Lucene.Net.Tartarus.Snowball.Ext
{
    /// <summary>
    /// This class was automatically generated by a Snowball to Java compiler
    /// It implements the stemming algorithm defined by a snowball script.
    /// </summary>
    public class ArmenianStemmer : SnowballProgram
    {
        // LUCENENET specific: Factored out methodObject by using Func<bool> instead of Reflection

        private readonly static Among[] a_0 = {
                    new Among ( "\u0580\u0578\u0580\u0564", -1, 1 ),
                    new Among ( "\u0565\u0580\u0578\u0580\u0564", 0, 1 ),
                    new Among ( "\u0561\u056C\u056B", -1, 1 ),
                    new Among ( "\u0561\u056F\u056B", -1, 1 ),
                    new Among ( "\u0578\u0580\u0561\u056F", -1, 1 ),
                    new Among ( "\u0565\u0572", -1, 1 ),
                    new Among ( "\u0561\u056F\u0561\u0576", -1, 1 ),
                    new Among ( "\u0561\u0580\u0561\u0576", -1, 1 ),
                    new Among ( "\u0565\u0576", -1, 1 ),
                    new Among ( "\u0565\u056F\u0565\u0576", 8, 1 ),
                    new Among ( "\u0565\u0580\u0565\u0576", 8, 1 ),
                    new Among ( "\u0578\u0580\u0567\u0576", -1, 1 ),
                    new Among ( "\u056B\u0576", -1, 1 ),
                    new Among ( "\u0563\u056B\u0576", 12, 1 ),
                    new Among ( "\u0578\u057E\u056B\u0576", 12, 1 ),
                    new Among ( "\u056C\u0561\u0575\u0576", -1, 1 ),
                    new Among ( "\u057E\u0578\u0582\u0576", -1, 1 ),
                    new Among ( "\u057A\u0565\u057D", -1, 1 ),
                    new Among ( "\u056B\u057E", -1, 1 ),
                    new Among ( "\u0561\u057F", -1, 1 ),
                    new Among ( "\u0561\u057E\u0565\u057F", -1, 1 ),
                    new Among ( "\u056F\u0578\u057F", -1, 1 ),
                    new Among ( "\u0562\u0561\u0580", -1, 1 )
                };

        private readonly static Among[] a_1 = {
                    new Among ( "\u0561", -1, 1 ),
                    new Among ( "\u0561\u0581\u0561", 0, 1 ),
                    new Among ( "\u0565\u0581\u0561", 0, 1 ),
                    new Among ( "\u057E\u0565", -1, 1 ),
                    new Among ( "\u0561\u0581\u0580\u056B", -1, 1 ),
                    new Among ( "\u0561\u0581\u056B", -1, 1 ),
                    new Among ( "\u0565\u0581\u056B", -1, 1 ),
                    new Among ( "\u057E\u0565\u0581\u056B", 6, 1 ),
                    new Among ( "\u0561\u056C", -1, 1 ),
                    new Among ( "\u0568\u0561\u056C", 8, 1 ),
                    new Among ( "\u0561\u0576\u0561\u056C", 8, 1 ),
                    new Among ( "\u0565\u0576\u0561\u056C", 8, 1 ),
                    new Among ( "\u0561\u0581\u0576\u0561\u056C", 8, 1 ),
                    new Among ( "\u0565\u056C", -1, 1 ),
                    new Among ( "\u0568\u0565\u056C", 13, 1 ),
                    new Among ( "\u0576\u0565\u056C", 13, 1 ),
                    new Among ( "\u0581\u0576\u0565\u056C", 15, 1 ),
                    new Among ( "\u0565\u0581\u0576\u0565\u056C", 16, 1 ),
                    new Among ( "\u0579\u0565\u056C", 13, 1 ),
                    new Among ( "\u057E\u0565\u056C", 13, 1 ),
                    new Among ( "\u0561\u0581\u057E\u0565\u056C", 19, 1 ),
                    new Among ( "\u0565\u0581\u057E\u0565\u056C", 19, 1 ),
                    new Among ( "\u057F\u0565\u056C", 13, 1 ),
                    new Among ( "\u0561\u057F\u0565\u056C", 22, 1 ),
                    new Among ( "\u0578\u057F\u0565\u056C", 22, 1 ),
                    new Among ( "\u056F\u0578\u057F\u0565\u056C", 24, 1 ),
                    new Among ( "\u057E\u0561\u056E", -1, 1 ),
                    new Among ( "\u0578\u0582\u0574", -1, 1 ),
                    new Among ( "\u057E\u0578\u0582\u0574", 27, 1 ),
                    new Among ( "\u0561\u0576", -1, 1 ),
                    new Among ( "\u0581\u0561\u0576", 29, 1 ),
                    new Among ( "\u0561\u0581\u0561\u0576", 30, 1 ),
                    new Among ( "\u0561\u0581\u0580\u056B\u0576", -1, 1 ),
                    new Among ( "\u0561\u0581\u056B\u0576", -1, 1 ),
                    new Among ( "\u0565\u0581\u056B\u0576", -1, 1 ),
                    new Among ( "\u057E\u0565\u0581\u056B\u0576", 34, 1 ),
                    new Among ( "\u0561\u056C\u056B\u057D", -1, 1 ),
                    new Among ( "\u0565\u056C\u056B\u057D", -1, 1 ),
                    new Among ( "\u0561\u057E", -1, 1 ),
                    new Among ( "\u0561\u0581\u0561\u057E", 38, 1 ),
                    new Among ( "\u0565\u0581\u0561\u057E", 38, 1 ),
                    new Among ( "\u0561\u056C\u0578\u057E", -1, 1 ),
                    new Among ( "\u0565\u056C\u0578\u057E", -1, 1 ),
                    new Among ( "\u0561\u0580", -1, 1 ),
                    new Among ( "\u0561\u0581\u0561\u0580", 43, 1 ),
                    new Among ( "\u0565\u0581\u0561\u0580", 43, 1 ),
                    new Among ( "\u0561\u0581\u0580\u056B\u0580", -1, 1 ),
                    new Among ( "\u0561\u0581\u056B\u0580", -1, 1 ),
                    new Among ( "\u0565\u0581\u056B\u0580", -1, 1 ),
                    new Among ( "\u057E\u0565\u0581\u056B\u0580", 48, 1 ),
                    new Among ( "\u0561\u0581", -1, 1 ),
                    new Among ( "\u0565\u0581", -1, 1 ),
                    new Among ( "\u0561\u0581\u0580\u0565\u0581", 51, 1 ),
                    new Among ( "\u0561\u056C\u0578\u0582\u0581", -1, 1 ),
                    new Among ( "\u0565\u056C\u0578\u0582\u0581", -1, 1 ),
                    new Among ( "\u0561\u056C\u0578\u0582", -1, 1 ),
                    new Among ( "\u0565\u056C\u0578\u0582", -1, 1 ),
                    new Among ( "\u0561\u0584", -1, 1 ),
                    new Among ( "\u0581\u0561\u0584", 57, 1 ),
                    new Among ( "\u0561\u0581\u0561\u0584", 58, 1 ),
                    new Among ( "\u0561\u0581\u0580\u056B\u0584", -1, 1 ),
                    new Among ( "\u0561\u0581\u056B\u0584", -1, 1 ),
                    new Among ( "\u0565\u0581\u056B\u0584", -1, 1 ),
                    new Among ( "\u057E\u0565\u0581\u056B\u0584", 62, 1 ),
                    new Among ( "\u0561\u0576\u0584", -1, 1 ),
                    new Among ( "\u0581\u0561\u0576\u0584", 64, 1 ),
                    new Among ( "\u0561\u0581\u0561\u0576\u0584", 65, 1 ),
                    new Among ( "\u0561\u0581\u0580\u056B\u0576\u0584", -1, 1 ),
                    new Among ( "\u0561\u0581\u056B\u0576\u0584", -1, 1 ),
                    new Among ( "\u0565\u0581\u056B\u0576\u0584", -1, 1 ),
                    new Among ( "\u057E\u0565\u0581\u056B\u0576\u0584", 69, 1 )
                };

        private readonly static Among[] a_2 = {
                    new Among ( "\u0578\u0580\u0564", -1, 1 ),
                    new Among ( "\u0578\u0582\u0575\u0569", -1, 1 ),
                    new Among ( "\u0578\u0582\u0570\u056B", -1, 1 ),
                    new Among ( "\u0581\u056B", -1, 1 ),
                    new Among ( "\u056B\u056C", -1, 1 ),
                    new Among ( "\u0561\u056F", -1, 1 ),
                    new Among ( "\u0575\u0561\u056F", 5, 1 ),
                    new Among ( "\u0561\u0576\u0561\u056F", 5, 1 ),
                    new Among ( "\u056B\u056F", -1, 1 ),
                    new Among ( "\u0578\u0582\u056F", -1, 1 ),
                    new Among ( "\u0561\u0576", -1, 1 ),
                    new Among ( "\u057A\u0561\u0576", 10, 1 ),
                    new Among ( "\u057D\u057F\u0561\u0576", 10, 1 ),
                    new Among ( "\u0561\u0580\u0561\u0576", 10, 1 ),
                    new Among ( "\u0565\u0572\u0567\u0576", -1, 1 ),
                    new Among ( "\u0575\u0578\u0582\u0576", -1, 1 ),
                    new Among ( "\u0578\u0582\u0569\u0575\u0578\u0582\u0576", 15, 1 ),
                    new Among ( "\u0561\u056E\u0578", -1, 1 ),
                    new Among ( "\u056B\u0579", -1, 1 ),
                    new Among ( "\u0578\u0582\u057D", -1, 1 ),
                    new Among ( "\u0578\u0582\u057D\u057F", -1, 1 ),
                    new Among ( "\u0563\u0561\u0580", -1, 1 ),
                    new Among ( "\u057E\u0578\u0580", -1, 1 ),
                    new Among ( "\u0561\u057E\u0578\u0580", 22, 1 ),
                    new Among ( "\u0578\u0581", -1, 1 ),
                    new Among ( "\u0561\u0576\u0585\u0581", -1, 1 ),
                    new Among ( "\u0578\u0582", -1, 1 ),
                    new Among ( "\u0584", -1, 1 ),
                    new Among ( "\u0579\u0565\u0584", 27, 1 ),
                    new Among ( "\u056B\u0584", 27, 1 ),
                    new Among ( "\u0561\u056C\u056B\u0584", 29, 1 ),
                    new Among ( "\u0561\u0576\u056B\u0584", 29, 1 ),
                    new Among ( "\u057E\u0561\u056E\u0584", 27, 1 ),
                    new Among ( "\u0578\u0582\u0575\u0584", 27, 1 ),
                    new Among ( "\u0565\u0576\u0584", 27, 1 ),
                    new Among ( "\u0578\u0576\u0584", 27, 1 ),
                    new Among ( "\u0578\u0582\u0576\u0584", 27, 1 ),
                    new Among ( "\u0574\u0578\u0582\u0576\u0584", 36, 1 ),
                    new Among ( "\u056B\u0579\u0584", 27, 1 ),
                    new Among ( "\u0561\u0580\u0584", 27, 1 )
                };

        private readonly static Among[] a_3 = {
                    new Among ( "\u057D\u0561", -1, 1 ),
                    new Among ( "\u057E\u0561", -1, 1 ),
                    new Among ( "\u0561\u0574\u0562", -1, 1 ),
                    new Among ( "\u0564", -1, 1 ),
                    new Among ( "\u0561\u0576\u0564", 3, 1 ),
                    new Among ( "\u0578\u0582\u0569\u0575\u0561\u0576\u0564", 4, 1 ),
                    new Among ( "\u057E\u0561\u0576\u0564", 4, 1 ),
                    new Among ( "\u0578\u057B\u0564", 3, 1 ),
                    new Among ( "\u0565\u0580\u0564", 3, 1 ),
                    new Among ( "\u0576\u0565\u0580\u0564", 8, 1 ),
                    new Among ( "\u0578\u0582\u0564", 3, 1 ),
                    new Among ( "\u0568", -1, 1 ),
                    new Among ( "\u0561\u0576\u0568", 11, 1 ),
                    new Among ( "\u0578\u0582\u0569\u0575\u0561\u0576\u0568", 12, 1 ),
                    new Among ( "\u057E\u0561\u0576\u0568", 12, 1 ),
                    new Among ( "\u0578\u057B\u0568", 11, 1 ),
                    new Among ( "\u0565\u0580\u0568", 11, 1 ),
                    new Among ( "\u0576\u0565\u0580\u0568", 16, 1 ),
                    new Among ( "\u056B", -1, 1 ),
                    new Among ( "\u057E\u056B", 18, 1 ),
                    new Among ( "\u0565\u0580\u056B", 18, 1 ),
                    new Among ( "\u0576\u0565\u0580\u056B", 20, 1 ),
                    new Among ( "\u0561\u0576\u0578\u0582\u0574", -1, 1 ),
                    new Among ( "\u0565\u0580\u0578\u0582\u0574", -1, 1 ),
                    new Among ( "\u0576\u0565\u0580\u0578\u0582\u0574", 23, 1 ),
                    new Among ( "\u0576", -1, 1 ),
                    new Among ( "\u0561\u0576", 25, 1 ),
                    new Among ( "\u0578\u0582\u0569\u0575\u0561\u0576", 26, 1 ),
                    new Among ( "\u057E\u0561\u0576", 26, 1 ),
                    new Among ( "\u056B\u0576", 25, 1 ),
                    new Among ( "\u0565\u0580\u056B\u0576", 29, 1 ),
                    new Among ( "\u0576\u0565\u0580\u056B\u0576", 30, 1 ),
                    new Among ( "\u0578\u0582\u0569\u0575\u0561\u0576\u0576", 25, 1 ),
                    new Among ( "\u0565\u0580\u0576", 25, 1 ),
                    new Among ( "\u0576\u0565\u0580\u0576", 33, 1 ),
                    new Among ( "\u0578\u0582\u0576", 25, 1 ),
                    new Among ( "\u0578\u057B", -1, 1 ),
                    new Among ( "\u0578\u0582\u0569\u0575\u0561\u0576\u057D", -1, 1 ),
                    new Among ( "\u057E\u0561\u0576\u057D", -1, 1 ),
                    new Among ( "\u0578\u057B\u057D", -1, 1 ),
                    new Among ( "\u0578\u057E", -1, 1 ),
                    new Among ( "\u0561\u0576\u0578\u057E", 40, 1 ),
                    new Among ( "\u057E\u0578\u057E", 40, 1 ),
                    new Among ( "\u0565\u0580\u0578\u057E", 40, 1 ),
                    new Among ( "\u0576\u0565\u0580\u0578\u057E", 43, 1 ),
                    new Among ( "\u0565\u0580", -1, 1 ),
                    new Among ( "\u0576\u0565\u0580", 45, 1 ),
                    new Among ( "\u0581", -1, 1 ),
                    new Among ( "\u056B\u0581", 47, 1 ),
                    new Among ( "\u057E\u0561\u0576\u056B\u0581", 48, 1 ),
                    new Among ( "\u0578\u057B\u056B\u0581", 48, 1 ),
                    new Among ( "\u057E\u056B\u0581", 48, 1 ),
                    new Among ( "\u0565\u0580\u056B\u0581", 48, 1 ),
                    new Among ( "\u0576\u0565\u0580\u056B\u0581", 52, 1 ),
                    new Among ( "\u0581\u056B\u0581", 48, 1 ),
                    new Among ( "\u0578\u0581", 47, 1 ),
                    new Among ( "\u0578\u0582\u0581", 47, 1 )
                };

        private static readonly char[] g_v = { (char)209, (char)4, (char)128, (char)0, (char)18 };

        private int I_p2;
        private int I_pV;

        private void copy_from(ArmenianStemmer other)
        {
            I_p2 = other.I_p2;
            I_pV = other.I_pV;
            base.CopyFrom(other);
        }

        private bool r_mark_regions()
        {
            int v_1;
            // (, line 58
            I_pV = m_limit;
            I_p2 = m_limit;
            // do, line 62
            v_1 = m_cursor;

            do
            {
                // (, line 62
                // gopast, line 63

                while (true)
                {

                    do
                    {
                        if (!(InGrouping(g_v, 1377, 1413)))
                        {
                            goto lab2;
                        }
                        goto golab1;
                    } while (false);
                    lab2:
                    if (m_cursor >= m_limit)
                    {
                        goto lab0;
                    }
                    m_cursor++;
                }
                golab1:
                // setmark pV, line 63
                I_pV = m_cursor;
                // gopast, line 63

                while (true)
                {

                    do
                    {
                        if (!(OutGrouping(g_v, 1377, 1413)))
                        {
                            goto lab4;
                        }
                        goto golab3;
                    } while (false);
                    lab4:
                    if (m_cursor >= m_limit)
                    {
                        goto lab0;
                    }
                    m_cursor++;
                }
                golab3:
                // gopast, line 64

                while (true)
                {

                    do
                    {
                        if (!(InGrouping(g_v, 1377, 1413)))
                        {
                            goto lab6;
                        }
                        goto golab5;
                    } while (false);
                    lab6:
                    if (m_cursor >= m_limit)
                    {
                        goto lab0;
                    }
                    m_cursor++;
                }
                golab5:
                // gopast, line 64

                while (true)
                {

                    do
                    {
                        if (!(OutGrouping(g_v, 1377, 1413)))
                        {
                            goto lab8;
                        }
                        goto golab7;
                    } while (false);
                    lab8:
                    if (m_cursor >= m_limit)
                    {
                        goto lab0;
                    }
                    m_cursor++;
                }
                golab7:
                // setmark p2, line 64
                I_p2 = m_cursor;
            } while (false);
            lab0:
            m_cursor = v_1;
            return true;
        }

        private bool r_R2()
        {
            if (!(I_p2 <= m_cursor))
            {
                return false;
            }
            return true;
        }

        private bool r_adjective()
        {
            int among_var;
            // (, line 72
            // [, line 73
            m_ket = m_cursor;
            // substring, line 73
            among_var = FindAmongB(a_0, 23);
            if (among_var == 0)
            {
                return false;
            }
            // ], line 73
            m_bra = m_cursor;
            switch (among_var)
            {
                case 0:
                    return false;
                case 1:
                    // (, line 98
                    // delete, line 98
                    SliceDel();
                    break;
            }
            return true;
        }

        private bool r_verb()
        {
            int among_var;
            // (, line 102
            // [, line 103
            m_ket = m_cursor;
            // substring, line 103
            among_var = FindAmongB(a_1, 71);
            if (among_var == 0)
            {
                return false;
            }
            // ], line 103
            m_bra = m_cursor;
            switch (among_var)
            {
                case 0:
                    return false;
                case 1:
                    // (, line 176
                    // delete, line 176
                    SliceDel();
                    break;
            }
            return true;
        }

        private bool r_noun()
        {
            int among_var;
            // (, line 180
            // [, line 181
            m_ket = m_cursor;
            // substring, line 181
            among_var = FindAmongB(a_2, 40);
            if (among_var == 0)
            {
                return false;
            }
            // ], line 181
            m_bra = m_cursor;
            switch (among_var)
            {
                case 0:
                    return false;
                case 1:
                    // (, line 223
                    // delete, line 223
                    SliceDel();
                    break;
            }
            return true;
        }

        private bool r_ending()
        {
            int among_var;
            // (, line 227
            // [, line 228
            m_ket = m_cursor;
            // substring, line 228
            among_var = FindAmongB(a_3, 57);
            if (among_var == 0)
            {
                return false;
            }
            // ], line 228
            m_bra = m_cursor;
            // call R2, line 228
            if (!r_R2())
            {
                return false;
            }
            switch (among_var)
            {
                case 0:
                    return false;
                case 1:
                    // (, line 287
                    // delete, line 287
                    SliceDel();
                    break;
            }
            return true;
        }


        public override bool Stem()
        {
            int v_1;
            int v_2;
            int v_3;
            int v_4;
            int v_5;
            int v_6;
            int v_7;
            // (, line 292
            // do, line 294
            v_1 = m_cursor;

            do
            {
                // call mark_regions, line 294
                if (!r_mark_regions())
                {
                    goto lab0;
                }
            } while (false);
            lab0:
            m_cursor = v_1;
            // backwards, line 295
            m_limit_backward = m_cursor; m_cursor = m_limit;
            // setlimit, line 295
            v_2 = m_limit - m_cursor;
            // tomark, line 295
            if (m_cursor < I_pV)
            {
                return false;
            }
            m_cursor = I_pV;
            v_3 = m_limit_backward;
            m_limit_backward = m_cursor;
            m_cursor = m_limit - v_2;
            // (, line 295
            // do, line 296
            v_4 = m_limit - m_cursor;

            do
            {
                // call ending, line 296
                if (!r_ending())
                {
                    goto lab1;
                }
            } while (false);
            lab1:
            m_cursor = m_limit - v_4;
            // do, line 297
            v_5 = m_limit - m_cursor;

            do
            {
                // call verb, line 297
                if (!r_verb())
                {
                    goto lab2;
                }
            } while (false);
            lab2:
            m_cursor = m_limit - v_5;
            // do, line 298
            v_6 = m_limit - m_cursor;

            do
            {
                // call adjective, line 298
                if (!r_adjective())
                {
                    goto lab3;
                }
            } while (false);
            lab3:
            m_cursor = m_limit - v_6;
            // do, line 299
            v_7 = m_limit - m_cursor;

            do
            {
                // call noun, line 299
                if (!r_noun())
                {
                    goto lab4;
                }
            } while (false);
            lab4:
            m_cursor = m_limit - v_7;
            m_limit_backward = v_3;
            m_cursor = m_limit_backward; return true;
        }

        public override bool Equals(object o)
        {
            return o is ArmenianStemmer;
        }

        public override int GetHashCode()
        {
            return this.GetType().FullName.GetHashCode();
        }
    }
}