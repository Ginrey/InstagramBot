﻿#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using InstagramBot.Net.Web;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace InstagramBot
{
    internal class Program
    {
        static Session Session;
        static void Main(string[] args)
        {
          
            Console.OutputEncoding = Encoding.UTF8;
            //310136073:AAHEP0i318aIkB8y3lAOTtwYxMf7jwtp51w - Hierarchy
            //326705762:AAHtag0VT3_wk0Hc1GCyrIyOL_OXeg-CAHQ - 110100
            string connectionString = "326705762:AAHtag0VT3_wk0Hc1GCyrIyOL_OXeg-CAHQ";
#if DEBUG
            connectionString = "326705762:AAHtag0VT3_wk0Hc1GCyrIyOL_OXeg-CAHQ";
#endif
            
             Session = new Session(connectionString)
            {
                ListWebInstagram = new List<WebInstagram>
                {
                   /* new WebInstagram("Instacc020", "36987412in", "146.185.208.35:3000", "mmGoRn:RdGbG1uN"),
                    new WebInstagram("Instacc021", "36987412in", "146.185.208.46:3000", "mmGoRn:RdGbG1uN"),
                    new WebInstagram("Instacc025", "36987412in", "146.185.209.184:3000", "mmGoRn:RdGbG1uN"),
                    //   new WebInstagram("Instacc032", "36987412in", "146.185.209.216:3000", "mmGoRn:RdGbG1uN"),*/
                    new WebInstagram("Instacc038", "36987412in", "5.188.56.37:3000", "mmGoRn:RdGbG1uN"),
                  //  new WebInstagram("Instacc039", "36987412in", "5.188.230.49:3000", "mmGoRn:RdGbG1uN")
                }
            };
            Session.Start();
            // Spam();
            var p = Session.WebInstagram.GetAccount("skew77");
            Thread.Sleep(Timeout.Infinite);
        }

        static List<long> l = new List<long>
        {
            324925672,
533614990,
347469891,
498763006,
556418133,
247120565,
345458158,
367563154,
494706223,
313310681,
160921701,
344934692,
299761525,
317167384,
382109837,
459702039,
228783751,
346233929,
176698513,
536313612,
394249614,
419318511,
488101083,
532927157,
222415239,
430645348,
490672865,
530604935,
375980801,
532312363,
42313120,
436121451,
553425233,
234914454,
394863556,
551971235,
591758796,
533614990,
551015849,
259303340,
318243563,
498139941,
372170799,
504604895,
41888079,
281016310,
400035209,
240892360,
361621949,
543610423,
586367104,
139315364,
137004464,
160980092,
515824686,
473421554,
352885861,
357382930,
374593597,
409672192,
337919473,
546721322,
349829772,
525302306,
500140863,
514402903,
508776474,
481087336,
438002548,
567846365,
498139941,
294810223,
492204430,
502398298,
335209387,
468893817,
337378311,
294957809,
276960605,
498095706,
210125957,
519624099,
477821599,
558993595,
289418439,
389496602,
69546502,
401115,
532513943,
489387090,
269784809,
414321337,
388172299,
348196064,
437886752,
337378311,
185461753,
245661319,
394781361,
513612424,
568051126,
417744937,
538764639,
399849248,
508289847,
527597770,
412084717,
493411636,
356588209,
332706934,
448436805,
493514069,
411108383,
495727258,
446168993,
390766434,
465291329,
412553888,
331030896,
99784654,
485530352,
431411850,
548513746,
357658668,
534229601,
533783995,
441082590,
344794174,
568004862,
292829949,
420611497,
366739714,
233198269,
412665662,
296464851,
474337013,
413707455,
513227637,
494479536,
264501839,
384091307,
246739628,
349188350,
251114956,
551672471,
526325432,
348503864,
239236186,
336701294,
352496845,
349402315,
569882905,
499436848,
317759113,
543381093,
468697149,
476799184,
466908543,
265487331,
336535054,
356286258,
512423781,
525569725,
511057967,
536070936,
383185352,
501129913,
401924608,
537715530,
345457941,
505599838,
396143512,
306476797,
487806803,
468539725,
545867468,
465173556,
468477433,
272873638,
491210273,
469038748,
247105597,
408860586,
384098120,
469840470,
431702543,
78362373,
206026146,
509604148,
518222230,
461351139,
432211006,
428773985,
508513907,
284105891,
369064134,
541718025,
354405234,
383213305,
533688410,
533688410,
501182329,
365389495,
451018513,
463403879,
441606980,
534756748,
508066890,
443326819,
474806463,
406543611,
405868236,
517880285,
496391381,
435415521,
483215452,
71894219,
441606980,
426641650,
355192600,
352640854,
329292200,
190982477,
154320371,
250675634,
542336368,
501319894,
305431343,
459253017,
562062703,
453289935,
527183137,
456106271,
464879501,
366824545,
554316787,
553936197,
514973383,
449357253,
527198329,
513128626,
340184148,
417788578,
175731529,
443323867,
483454757,
367543795,
458040406,
544824455,
383220872,
534247606,
283477220,
469797136,
385161853,
289990692,
207334823,
338048546,
343619936,
329967928,
245586799,
528575286,
195085995,
417463107,
493065121,
412693190,
505863137,
391829342,
474703183,
372939367,
462777223,
523063767,
556413473,
512372070,
459074908,
534451240,
258530234,
415956197,
507140362,
350279784,
241935951,
449357253,
569377013,
568510879,
318382041,
497033198,
419522960,
214220765,
292576211,
412448356,
563563483,
423041119,
449724501,
244827233,
512026318,
553864916,
456925573,
568428205,
192214677,
552790965,
347952333,
351175156,
479457099,
407483013,
530964787,
347364987,
279415239,
480604833,
563802441,
253666941,
555805911,
288813969,
508743976,
546337888,
368759455,
495882341,
470132281,
455706796,
433824677,
452146899,
536773733,
392312720,
269514474,
538773289,
508938830,
427790533,
411077645,
337671956,
562300552,
419180324,
357732809,
519967962,
387361337,
371046021,
504286465,
545057154,
419180324,
337553090,
463407656,
334448046,
145956595,
336166029,
239289972,
546627852,
206118088,
280347982,
300319901,
526975147,
92764421,
525676078,
567856106,
208332515,
567864383,
390731113,
539716902,
537611178,
548164213,
432402613,
304641970,
363369189,
318672536,
568248655,
332175906,
163173129,
503835244,
512159287,
535690758,
274232565,
504930537,
413440205,
453861638,
518171999,
248882476,
556941377,
478052896,
384224353,
474882828,
535471301,
191392942,
380629377,
555180158,
452972548,
461071637,
453592298,
530456808,
373486629,
415786624,
112664878,
396666142,
500114657,
398654853,
378489163,
532120253,
551944212,
437890353,
549598471,
384805319,
501729994,
261900114,
467550293,
496534668,
379034482,
482595414,
166550597,
546230340,
568555719,
489425178,
145860197,
417098324,
365318162,
487455753,
421284650,
418395520,
559086820,
333205448,
520581330,
314787720,
464931170,
371107963,
539672591,
513511088,
557903024,
468997337,
452517193,
543292441,
429450447,
458507116,
423165214,
481285050,
481556420,
354891986,
498362273,
363708253,
530686993,
511339081,
479953227,
388016727,
544457023,
501401108,
310682961,
550781934,
99096316,
558389548,
468798783,
498761015,
299492541,
379574108,
245030104,
274810098,
563822693,
525115836,
515076926,
568689237,
530119733,
548935960,
195267498,
187209218,
541448484,
481171634,
551518086,
502232786,
487300252,
333334315,
388690049,
556040939,
274732817,
461952343,
384334154,
355579575,
417367768,
416977534,
461338815,
390082659,
489508782,
308816670,
544607469,
318649095,
565142763,
487409040,
523574629,
550368166,
388754528,
420933196,
527539241,
528318455,
272907105,
508559638,
544929708,
185066874,
294810223,
297799795,
360438891,
375545965,
455363840,
328509282,
531425187,
422045125,
290250581,
434307199,
182957521,
433538798,
432115372,
152306622,
400043210,
159667006,
421581813,
433088247,
387287459,
278331203,
454565462,
327493314,
237675725,
462560345,
550890530,
517949144,
265772371,
407849506,
349160863,
473326863,
460986708,
414386431,
326299725,
543537934,
451513558,
245228635,
380264171,
258718537,
532014128,
547664209,
543149551,
403969735,
499208733,
507651317,
310519813,
494258271,
416368970,
420827365,
536904282,
543972002,
289304508,
299942491,
875100,
393120128,
154797699,
471529637,
453713228,
139985659,
422770608,
405095626,
279971069,
464348040,
465248771,
527549901,
347704588,
416184275,
282000887,
303249642,
326634374,
501489400,
427970410,
459888152,
379775325,
566232254,
567181815,
466040991,
188507327,
76524897,
236078524,
501135333,
253989045,
394481367,
514001343,
230808454,
526814274,
502212960,
491129206,
489492794,
553807390,
474341250,
400105191,
256032930,
284634668,
535934782,
424327510,
529754568,
441956032,
466890001,
504643982,
538060258,
438057973,
325076387,
371830404,
453769813,
482613026,
509934309,
289928184,
507263781,
497123077,
521494089,
502625710,
255130440,
507295763,
355807872,
458539281,
261964435,
567258115,
393940388,
375171422,
138314801,
416008999,
372391428,
550821418,
391352741,
303718802,
500742477,
305291072,
513681946,
409865043,
526886480,
488427544,
243957648,
476736029,
252423987,
282683420,
358426598,
479780514,
346107121,
553965957,
387944027,
251663527,
422886695,
433804423,
387231624,
358413503,
529878979,
532644003,
529712067,
481912096,
528018265,
150382402,
426343951,
519012388,
564141436,
560035320,
513651175,
517758875,
528558048,
309062864,
558292406,
345284388,
563990743,
412206976,
491299695,
252324121,
511449611,
400641211,
511789400,
535494248,
413636695,
472479619,
547313033,
335557693,
276908901,
453282829,
196417172,
146961990,
533139533,
408161104,
389430159,
516172528,
462387941,
240617909,
548695357,
429437022,
528512512,
501196350,
308326074,
379162521,
488746651,
432089390,
317908501,
450716045,
289756826,
373482207,
190301188,
408569598,
506551963,
433610041,
394700024,
515222174,
561203604,
450290438,
432604611,
523382887,
434225047,
359153079,
476381885,
415988095,
331171039,
451524998,
508589396,
450145613,
476909447,
547904514,
545313717,
493991083,
502289204,
502179861,
326013973,
332514956,
442533257,
329146981,
494215728,
474792220,
475928441,
504888938,
453297183,
541172756,
518285478,
525779258,
240467090,
493671248,
519103452,
429712408,
502382093,
503239012,
544141498,
569429886,
377277301,
569368158,
436183184,
435586875,
520296744,
452414877,
449006360,
473103947,
484674833,
499917337,
518709974,
344058007,
544612229,
418545757,
552226768,
554611319,
520522822,
379433635,
535154745,
399381017,
406546974,
301303092,
444753499,
406225934,
414934769,
62884298,
367468119,
480610620,
404251915,
499677454,
468211910,
494825146,
400026682,
542535227,
504601918,
524972423,
420276246,
367982010,
442234048,
229679394,
499066241,
493767265,
516502522,
442130117,
524967692,
358755356,
336450892,
493629256,
416643870,
553380700,
470577889,
499726254,
359816820,
199914062,
534598813,
446863482,
488605586,
475063119,
450272984,
422387434,
367793236,
447485234,
457645530,
462613639,
410406796,
444427931,
379176659,
478123689,
456498641,
406665964,
517429408,
391001805,
513159840,
372664535,
360234931,
337784374,
385614896,
362799533,
326330911,
420474717,
557399404,
433909569,
383115340,
538523282,
239522546,
394898125,
508321342,
523539111,
173741672,
267172830,
548117110,
284140852,
475570480,
391907328,
492828668,
452832940,
558382712,
460947287,
24509825,
484389441,
271068515,
479691965,
469714805,
425207094,
305803563,
474107488,
200158200,
565644563,
542989336,
370579198,
512716455,
505615144,
479523153,
536485089,
441399219,
366299112,
365089178,
443996546,
450638173,
439065480,
551954324,
382603452,
93368592,
375749057,
453260306,
453089653,
171518053,
413991507,
414802204,
348229067,
266416346,
284365518,
530476976,
446858333,
554244523,
472182335,
381249180,
479646905,
458771272,
400592176,
397646991,
536093706,
524183526,
447927878,
431274558,
495462491,
396054006,
528967927,
344685905,
461989131,
544079189,
558600989,
414324506,
538882203,
501402407,
359983855,
450306041,
233131202,
484663694,
463895297,
567859043,
396600606,
273818743,
188701134,
518951140,
543470817,
397566743,
287285780,
524336173,
468246741,
474167307,
498833729,
419860145,
509665010,
450724424,
474090438,
322532279,
393821964,
456658876,
234214378,
568020285,
347545631,
433610651,
404689797,
394875208,
426942407,
462112607,
210119815,
533024306,
543035823,
450399973,
544686940,
483073181,
343206607,
564684841,
481120251,
555018208,
552404964,
543675199,
335010695,
210111999,
485289984,
490243526,
419966514,
437489837,
424561387,
557000388,
161907635,
550352536,
454170805,
467595678,
253266368,
460211075,
507275087,
548696123,
364911006,
337020373,
516883008,
401977829,
518564212,
489167096,
479493087,
410724620,
416705045,
435942175,
455638635,
468155219,
238908488,
545201021,
521637819,
470919479,
423779755,
210866073,
404831426,
230546707,
404486118,
233267548,
558929348,
325561335,
291348704,
485314958,
426818502,
156392849,
474005345,
492423557,
500101699,
566759712,
527308498,
454356249,
372936751,
396600606,
544250626,
320028883,
433992706,
430211551,
307919312,
271349310,
432555609,
536438567,
311296304,
427239167,
505966924,
445675964,
501052462,
437350233,
452178268,
125967221,
462456377,
503646048,
485551756,
542543203,
346232894,
467755939
        };
        public static async  void Spam()
        {
            int index = 0;
            foreach (var l1 in l)
            {
                try
                {
                    Session.Bot.SendTextMessageAsync(l1, @"Вы тратите свои деньги каждый день.
А мы возвращаем потраченное.
💶 КЕШБЭК 💳
Это работает.
Убедитесь сами
..................................
Подробности здесь:
    👇🏻      👇🏻      👇🏻
t.me/WWPCauto_bot");
                    Session.Bot.SendPhotoAsync(l1, "AgADAgADyagxG-57aUocchRuNx0bUkQTnA4ABAGidHvSjAO8nXUDAAEC");
                    index++;
                    if(index > 15)
                    {
                        await Task.Delay(1500);
                        index = 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    
                }
            }
        }
       
    }
}