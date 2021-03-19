
using System;
using System.Collections.Generic;
using SolrHTTP.Docs;

namespace SolrHTTP.Helper {
    public class SolrDataTypeConvert {

    private static readonly Dictionary<string, Type> _dictionary_mulitval_false = new() {
        
        {"boolean", typeof(bool) },
        {"booleans", typeof(bool) },
        
        {"pint", typeof(int) },
        {"pints", typeof(int[]) },

        {"plong", typeof(long) },
        {"plongs", typeof(long[]) },

        {"pfloat", typeof(float) },
        {"pfloats", typeof(float[]) },

        {"pdouble", typeof(double) },
        {"pdoubles", typeof(double[]) },

        {"string", typeof(string) },
        {"strings", typeof(string[]) },
        {"text_general", typeof(string) },
        {"text_ar", typeof(string) },
        {"text_bg", typeof(string) },
        {"text_ca", typeof(string) },
        {"text_cjk", typeof(string) },
        {"text_cz", typeof(string) },        
        {"text_da", typeof(string) },
        {"text_de", typeof(string) },
        {"text_el", typeof(string) },
        {"text_en", typeof(string) },
        {"text_es", typeof(string) },
        {"text_et", typeof(string) },
        {"text_eu", typeof(string) },
        {"text_fa", typeof(string) },
        {"text_fi", typeof(string) },
        {"text_fr", typeof(string) },
        {"text_ga", typeof(string) },
        {"text_gl", typeof(string) },
        {"text_hi", typeof(string) },
        {"text_hu", typeof(string) },
        {"text_hy", typeof(string) },
        {"text_id", typeof(string) },
        {"text_it", typeof(string) },
        {"text_ja", typeof(string) },
        {"text_ko", typeof(string) },
        {"text_lv", typeof(string) },
        {"text_nl", typeof(string) },
        {"text_no", typeof(string) },
        {"text_pt", typeof(string) },
        {"text_ro", typeof(string) },
        {"text_ru", typeof(string) },
        {"text_sv", typeof(string) },
        {"text_th", typeof(string) },
        {"text_tr", typeof(string) },
        {"text_ws", typeof(string) },
      
    };

        public static Type TranslateSolrField( string solrType) {

            Type type;

            type = null;

            if (_dictionary_mulitval_false.ContainsKey(solrType)) {
               type = _dictionary_mulitval_false[solrType];
            } else {
               // throw new NotSupportedException();
            }
            
            /*
            switch(solrType) {                                    
                case "_nest_path_":
                case "ancestor_path":
                case "binary":
                case "delimited_payloads_float":
                case "delimited_payloads_int":
                case "delimited_payloads_string":
                case "descendent_path":
                case "ignored":
                case "location":
                case "location_rpt":
                case "lowercase":
                case "pdate":
                case "pdates":
                case "phonetic_en":
                case "point":                
                case "random":
                case "rank":                
                case "text_en_splitting":
                case "text_en_splitting_tight":                
                case "text_gen_sort":
                case "text_general":
                case "text_general_rev":                                
                default:
                                    
                   // throw new NotSupportedException();
                    break;    
                }          
            */
            return type;
        }
    }

}