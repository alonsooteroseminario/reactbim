/*
 * Created by SharpDevelop.
 * User: Luis Alonso Otero
 * Date: 11-07-2020
 * Time: 2:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization; 
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Lighting;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.DB.Visual;
using forms = System.Windows.Forms;

namespace reactbim
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("02AF7CF8-31EB-4724-B764-ED101DC1A197")]
	public partial class ThisApplication
	{
		private void Module_Startup(object sender, EventArgs e)
		{

		}

		private void Module_Shutdown(object sender, EventArgs e)
		{

		}

		#region Revit Macros generated code
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(Module_Startup);
			this.Shutdown += new System.EventHandler(Module_Shutdown);
		}
		#endregion
		
		
		public void place_WallsDoorsWindows()
		{
			
			UIDocument uidoc = this.ActiveUIDocument;
			Document doc = uidoc.Document;
			
			// Get Active View
			View activeView = this.ActiveUIDocument.ActiveView;
			
			
			//<<----------------------------------------------------INPUTS---------------------------------------------------------->>
			
			//level
			Level level = GetLevel("Plan 06"); // 1.
			
			// coordenadas XYZ(x,y,z)
			XYZ stPoint = new XYZ(0,0,0); // 2.
			XYZ endPoint = new XYZ(0,0,0); // 3.
			
			// Dimensions
			// type of House Shapes :  square, rectangle, triangle
			string n = "rectangle"; // 4.
			
			double _heigth_ = 2880/304.8; // 7.
			
			// coord XYZ(x,y,z)
			XYZ stPoint_nuevo = new XYZ(0,0,0);
			
			//<<------------------------------------------------------INPUTS-------------------------------------------------------->>
			
			
			// Create House in differents Shapes :  Square, Rectangle, Triangle
			List<List<Wall>> walls = Create_House_Shapes(n, _heigth_, stPoint_nuevo, "Plan 06");
			
			List<Wall> walls_Rigth = walls.First();
			List<Wall> walls_Left = walls.Last();
			
//			TaskDialog.Show("REACT-BIM", aa_Rigth.Count().ToString() + " Walls in the Rigth" + Environment.NewLine + 
//			                									aa_Left.Count().ToString() + " Walls in the Left");
			


		}
		
		
				// inputs: n=shape 
		public List<List<Wall>> Create_House_Shapes(string n, double _heigth_, XYZ startPoint, string levelName)
		{
			UIDocument uidoc = this.ActiveUIDocument;
			Document doc = uidoc.Document;
						
			// Get Active View
			View activeView = this.ActiveUIDocument.ActiveView;
			
			//list walls output
			List<Wall> output_wall_Rigth = new List<Wall>();
			List<Wall> output_wall_Left = new List<Wall>();
			
			List<List<Wall>> output = new List<List<Wall>>();

			//inputs
			
			//level
			Level level = GetLevel(levelName);
			
			// coord XYZ(x,y,z)
			XYZ stPoint = startPoint;
			
			
			Transaction trans = new Transaction(doc);

			trans.Start("Create House");
			
			if (n == "square") {
				
				#region square
				TaskDialog.Show("ALERTA", "--------------------------------------------------------------------------");

				double heigth_double = _heigth_;

				// Crear Wall Primer
				
				XYZ endPoint = new XYZ(30,0,0);
				XYZ endPoint_2 = new XYZ(30, 30, 0);
				XYZ endPoint_3 = new XYZ(0, 30, 0 );
				
				Line newLineN = Line.CreateBound(stPoint, endPoint);
				Line newLineN_2 = Line.CreateBound(endPoint, endPoint_2);
				Line newLineN_3 = Line.CreateBound(endPoint_2, endPoint_3);
				Line newLineN_4 = Line.CreateBound(endPoint_3, stPoint);
				
				Wall wall = Wall.Create(doc, newLineN, level.Id, false);
				Wall wall_2 = Wall.Create(doc, newLineN_2, level.Id, false);
				Wall wall_3 = Wall.Create(doc, newLineN_3, level.Id, false);
				Wall wall_4 = Wall.Create(doc, newLineN_4, level.Id, false);
				
				List<Wall> lista_walls = new List<Wall>();
				
				lista_walls.Add(wall);
				lista_walls.Add(wall_2);
				lista_walls.Add(wall_3);
				lista_walls.Add(wall_4);
				
				output_wall_Rigth.Add(wall_2);
				output_wall_Rigth.Add(wall_4);
				output_wall_Left.Add(wall);
				output_wall_Left.Add(wall_3);
				
				output.Add(output_wall_Rigth);
				output.Add(output_wall_Left);
				
				
				foreach (Wall e in lista_walls) 
				{
					Parameter height = e.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM);
					if (!height.IsReadOnly)
					{
						height.Set(heigth_double);
					}
					
					if (WallUtils.IsWallJoinAllowedAtEnd(e, 1))
									WallUtils.DisallowWallJoinAtEnd(e, 1);
		
					if (WallUtils.IsWallJoinAllowedAtEnd(e, 0))
									WallUtils.DisallowWallJoinAtEnd(e, 0);
					
					
				}
				
				#endregion
				
			}
			else if ( n == "rectangle") {
				
				#region rectangle

				double heigth_double = _heigth_;

				// Crear Wall Primer
				
				XYZ endPoint = new XYZ(20,0,0);
				XYZ endPoint_2 = new XYZ(20, 10, 0);
				XYZ endPoint_3 = new XYZ(0, 10, 0 );
				
				Line newLineN = Line.CreateBound(stPoint, endPoint);
				Line newLineN_2 = Line.CreateBound(endPoint, endPoint_2);
				Line newLineN_3 = Line.CreateBound(endPoint_2, endPoint_3);
				Line newLineN_4 = Line.CreateBound(endPoint_3, stPoint);
				
				Wall wall = Wall.Create(doc, newLineN, level.Id, false);
				Wall wall_2 = Wall.Create(doc, newLineN_2, level.Id, false);
				Wall wall_3 = Wall.Create(doc, newLineN_3, level.Id, false);
				Wall wall_4 = Wall.Create(doc, newLineN_4, level.Id, false);
				
				List<Wall> lista_walls = new List<Wall>();
				
				lista_walls.Add(wall);
				lista_walls.Add(wall_2);
				lista_walls.Add(wall_3);
				lista_walls.Add(wall_4);
				
				output_wall_Rigth.Add(wall_2);
				output_wall_Rigth.Add(wall_4);
				output_wall_Left.Add(wall);
				output_wall_Left.Add(wall_3);
				
				output.Add(output_wall_Rigth);
				output.Add(output_wall_Left);
				
				foreach (Wall e in lista_walls) 
				{
					Parameter height = e.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM);
					if (!height.IsReadOnly)
					{
						height.Set(heigth_double);
					}
					
					if (WallUtils.IsWallJoinAllowedAtEnd(e, 1))
									WallUtils.DisallowWallJoinAtEnd(e, 1);
		
					if (WallUtils.IsWallJoinAllowedAtEnd(e, 0))
									WallUtils.DisallowWallJoinAtEnd(e, 0);
					

				}
				
				#endregion
				
			}
			else if (n == "triangle") {
				
				#region triangle
				
				
				double heigth_double = _heigth_;

				
				// Crear Wall Primer
				
				XYZ endPoint = new XYZ(20,0,0);
				XYZ endPoint_2 = new XYZ(10, 10*1.7320508075, 0);

				Line newLineN = Line.CreateBound(stPoint, endPoint);
				Line newLineN_2 = Line.CreateBound(endPoint, endPoint_2);
				Line newLineN_3 = Line.CreateBound(endPoint_2, stPoint);

				
				Wall wall = Wall.Create(doc, newLineN, level.Id, false);
				Wall wall_2 = Wall.Create(doc, newLineN_2, level.Id, false);
				Wall wall_3 = Wall.Create(doc, newLineN_3, level.Id, false);

				
				List<Wall> lista_walls = new List<Wall>();
				
				lista_walls.Add(wall);
				lista_walls.Add(wall_2);
				lista_walls.Add(wall_3);
				
				output_wall_Rigth.Add(wall);
				output_wall_Rigth.Add(wall_2);
				output_wall_Rigth.Add(wall_3);
				
				output.Add(output_wall_Rigth);

				foreach (Wall e in lista_walls) 
				{
					Parameter height = e.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM);
					if (!height.IsReadOnly)
					{
						height.Set(heigth_double);
					}
					
					if (WallUtils.IsWallJoinAllowedAtEnd(e, 1))
									WallUtils.DisallowWallJoinAtEnd(e, 1);
		
					if (WallUtils.IsWallJoinAllowedAtEnd(e, 0))
									WallUtils.DisallowWallJoinAtEnd(e, 0);
				}
				
				#endregion
			}
			
			trans.Commit();

			
			return output;

		}
		
		
		// input: "Nivel 1"
		public Level GetLevel(string input)
		{
			Document doc = ActiveUIDocument.Document;			
			View active = doc.ActiveView;
			
			
			// INPUT LEVEL
			FilteredElementCollector lvlCollector = new FilteredElementCollector(doc);
			ICollection<Element> lvlCollection = lvlCollector.OfClass(typeof(Level)).ToElements();
			
			string msg = "";
			
			List<Level> salida = new List<Level>();
			foreach (Element l in lvlCollection)
			{			

				msg = msg + l.Name.ToString()  + "\n" + Environment.NewLine ;
				if (l.Name.ToString() == input) {
					salida.Add(l as Level);
				}	

			}
			
			
			//TaskDialog.Show("test", salida.First().Name.ToString());
			
			return salida.First();
			
		}
		

		

			
		public Wall Revision6_DYNO_Girar180_Muro_ConVentanas_nuevo(Element _e_)
			{
				UIDocument uidoc = this.ActiveUIDocument;
				Document doc = uidoc.Document;

				// Get Active View
				View activeView = this.ActiveUIDocument.ActiveView;

				List<Wall> listaWalls_Final = new List<Wall>();

				//			Element e = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element)); // Wall que se elige
				Wall wall_1 = _e_ as Wall; // muro actual

				Curve wallCurve = ((LocationCurve)wall_1.Location).Curve;

				double stParam = wallCurve.GetEndParameter(0);
				double endParam = wallCurve.GetEndParameter(1);

				Parameter longi = wall_1.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
				double longi_double = longi.AsDouble(); // 1220


				XYZ stPoint = wallCurve.Evaluate(stParam, false);
				XYZ endPoint = wallCurve.Evaluate(endParam, false);


				#region INFO VENTANAS


				// Recolectar Ventanas
				ElementClassFilter familyFilter = new ElementClassFilter(typeof(FamilyInstance));
				ElementCategoryFilter MECategoryfilter = new ElementCategoryFilter(BuiltInCategory.OST_Windows);
				LogicalAndFilter MEInstancesFilter = new LogicalAndFilter(familyFilter, MECategoryfilter);
				FilteredElementCollector coll = new FilteredElementCollector(doc, activeView.Id);

				IList<Element> windows = coll.WherePasses(MEInstancesFilter).ToElements(); // todas las ventadas

				List<Element> windows_hosted = new List<Element>();

				foreach (Element elem in windows)
				{
					FamilyInstance fintance = elem as FamilyInstance;

					if (fintance.Host.Id == _e_.Id)
					{
						windows_hosted.Add(elem);
					}

				}

				List<Element> windows_hosted_sorted = windows_hosted.OrderBy(x => (x as FamilyInstance).HostParameter).ToList(); // menor a mayor

				List<double> lista_a = new List<double>();
				List<double> lista_width1 = new List<double>();
				List<double> lista_heigth1 = new List<double>();
				List<double> lista_win_sill_height1 = new List<double>();
				List<double> lista_dPH = new List<double>();
				List<FamilySymbol> lista_FamilySymbol = new List<FamilySymbol>();


				foreach (Element win in windows_hosted_sorted)
				{
					FamilyInstance win1 = win as FamilyInstance;
					FamilySymbol familySymbol = win1.Symbol;
					lista_FamilySymbol.Add(familySymbol);

					double dPH1 = win1.HostParameter; //3700
					lista_dPH.Add(dPH1);

					double a0 = endParam - dPH1;
					lista_a.Add(a0);

					ElementType type1 = doc.GetElement(win1.GetTypeId()) as ElementType;
					Parameter widthParam1 = type1.LookupParameter("Width"); // ancho ventana 1220
					Parameter heightParam1 = type1.LookupParameter("Height"); // altura ventana 1240


					double width1 = widthParam1.AsDouble(); // ancho ventana 1220
					lista_width1.Add(width1);
					double heigth1 = heightParam1.AsDouble(); // altura ventana 1240
					lista_heigth1.Add(heigth1);
					double win_sill_height1 = win1.get_Parameter(BuiltInParameter.INSTANCE_SILL_HEIGHT_PARAM).AsDouble(); // 800
					lista_win_sill_height1.Add(win_sill_height1);


					double win_head_height1 = win1.get_Parameter(BuiltInParameter.INSTANCE_HEAD_HEIGHT_PARAM).AsDouble(); // 2040

				}

				#endregion

				Transaction trans = new Transaction(doc);

				trans.Start("mysplitwall");

				// CORREGIR WALL 1 EXISTENTE


				if (WallUtils.IsWallJoinAllowedAtEnd(wall_1, 1))
					WallUtils.DisallowWallJoinAtEnd(wall_1, 1);



				// agregar ventanas en el restante
				for (int i = 0; i < lista_dPH.Count(); i++)
				{

					double dPH1_nuevo = lista_dPH[i];

					FamilySymbol familySymbol = lista_FamilySymbol[i];



					XYZ xyz_dPH1 = wallCurve.Evaluate(dPH1_nuevo, false);

					XYZ xyz = new XYZ(xyz_dPH1.X, xyz_dPH1.Y, lista_win_sill_height1[i]);

					// Create window.

					if (!familySymbol.IsActive)
					{
						// Ensure the family symbol is activated.
						familySymbol.Activate();
						doc.Regenerate();
					}

					// Create window
					// unliss you specified a host, Rebit will create the family instance as orphabt object.
					FamilyInstance window = doc.Create.NewFamilyInstance(xyz, familySymbol, wall_1, StructuralType.NonStructural);

				}



				trans.Commit();



				return wall_1;

			}

			
		
		
        public void CreateWindow(Wall _wall_, double _height_, string fsFamilyName = "M_VANO VENTANA", string fsName = "1220x1240", string levelName = "Nivel 1")
        {
            
			UIDocument uidoc = this.ActiveUIDocument;
			Document doc = uidoc.Document;

			// Get Active View
			View activeView = this.ActiveUIDocument.ActiveView;

//           	string fsFamilyName = "M_VANO VENTANA";
//            string fsName = "1220x1240";
//            string levelName = "Nivel 1";
            
            Wall wall = _wall_ as Wall; // muro actual

			Curve wallCurve = ((LocationCurve)wall.Location).Curve;

			double stParam = wallCurve.GetEndParameter(0);
			double endParam = wallCurve.GetEndParameter(1);

			Parameter longi = wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
			double longi_double = longi.AsDouble(); // 1220
			
			double h_window = _height_;
			
			double midle_wall_longi = stParam + longi_double/2;
			
			XYZ point_midle_wall_longi = wallCurve.Evaluate(midle_wall_longi, false);


			XYZ stPoint = wallCurve.Evaluate(stParam, false);
			XYZ endPoint = wallCurve.Evaluate(endParam, false);
            
				

			// LINQ to find the window's FamilySymbol by its type name.
            FamilySymbol familySymbol = (from fs in new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).Cast<FamilySymbol>()
                                         where (fs.Family.Name == fsFamilyName && fs.Name == fsName)
                                         select fs).First();

            // LINQ to find the level by its name.
            Level level = (from lvl in new FilteredElementCollector(doc).
                           OfClass(typeof(Level)).
                           Cast<Level>()
                           where (lvl.Name == levelName)
                           select lvl).First();
            
            
            

            // Convert coordinates to double and create XYZ point.

            XYZ xyz = new XYZ(point_midle_wall_longi.X, point_midle_wall_longi.Y, level.Elevation + h_window);


            // Create window.
            using (Transaction t = new Transaction(doc, "Create window"))
            {
                t.Start();

                if (!familySymbol.IsActive)
                {
                    // Ensure the family symbol is activated.
                    familySymbol.Activate();
                    doc.Regenerate();
                }

                // Create window
                // unliss you specified a host, Rebit will create the family instance as orphabt object.
                FamilyInstance window = doc.Create.NewFamilyInstance(xyz, familySymbol, wall, StructuralType.NonStructural);
                t.Commit();
            }
//            string prompt = "The element was created!";
//            TaskDialog.Show("Revit", prompt);
        }
        
        
        public void CreateDoor(Wall _wall_)
        {
            
			UIDocument uidoc = this.ActiveUIDocument;
			Document doc = uidoc.Document;

			// Get Active View
			View activeView = this.ActiveUIDocument.ActiveView;

           	string fsFamilyName = "M_VANO PUERTA";
            string fsName = "750x2050";
            string levelName = "Nivel 1";
            
            Wall wall = _wall_ as Wall; // muro actual

			Curve wallCurve = ((LocationCurve)wall.Location).Curve;

			double stParam = wallCurve.GetEndParameter(0);
			double endParam = wallCurve.GetEndParameter(1);

			Parameter longi = wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
			double longi_double = longi.AsDouble(); // 1220
			
			double midle_wall_longi = stParam + longi_double/2;
			
			XYZ point_midle_wall_longi = wallCurve.Evaluate(midle_wall_longi, false);


			XYZ stPoint = wallCurve.Evaluate(stParam, false);
			XYZ endPoint = wallCurve.Evaluate(endParam, false);
            
				

			// LINQ to find the window's FamilySymbol by its type name.
            FamilySymbol familySymbol = (from fs in new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).Cast<FamilySymbol>()
                                         where (fs.Family.Name == fsFamilyName && fs.Name == fsName)
                                         select fs).First();

            // LINQ to find the level by its name.
            Level level = (from lvl in new FilteredElementCollector(doc).
                           OfClass(typeof(Level)).
                           Cast<Level>()
                           where (lvl.Name == levelName)
                           select lvl).First();
            
            
            

            // Convert coordinates to double and create XYZ point.

            XYZ xyz = new XYZ(point_midle_wall_longi.X, point_midle_wall_longi.Y, level.Elevation);

            #region 

            #endregion

            // Create window.
            using (Transaction t = new Transaction(doc, "Create window"))
            {
                t.Start();

                if (!familySymbol.IsActive)
                {
                    // Ensure the family symbol is activated.
                    familySymbol.Activate();
                    doc.Regenerate();
                }

                // Create window
                // unliss you specified a host, Rebit will create the family instance as orphabt object.
                FamilyInstance window = doc.Create.NewFamilyInstance(xyz, familySymbol, wall, StructuralType.NonStructural);
                t.Commit();
            }
//            string prompt = "The element was created!";
//            TaskDialog.Show("Revit", prompt);
        }
			
        
        public void CreateDoor(Wall _wall_, string fsFamilyName = "M_VANO PUERTA", string fsName = "750x2050", string levelName = "Nivel 1" )
        {
            
			UIDocument uidoc = this.ActiveUIDocument;
			Document doc = uidoc.Document;

			// Get Active View
			View activeView = this.ActiveUIDocument.ActiveView;

//           	string fsFamilyName = "M_VANO PUERTA";
//            string fsName = "750x2050";
//            string levelName = "Nivel 1";
            
            Wall wall = _wall_ as Wall; // muro actual

			Curve wallCurve = ((LocationCurve)wall.Location).Curve;

			double stParam = wallCurve.GetEndParameter(0);
			double endParam = wallCurve.GetEndParameter(1);

			Parameter longi = wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
			double longi_double = longi.AsDouble(); // 1220
			
			double midle_wall_longi = stParam + longi_double/2;
			
			XYZ point_midle_wall_longi = wallCurve.Evaluate(midle_wall_longi, false);


			XYZ stPoint = wallCurve.Evaluate(stParam, false);
			XYZ endPoint = wallCurve.Evaluate(endParam, false);
            
				

			// LINQ to find the window's FamilySymbol by its type name.
            FamilySymbol familySymbol = (from fs in new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).Cast<FamilySymbol>()
                                         where (fs.Family.Name == fsFamilyName && fs.Name == fsName)
                                         select fs).First();

            // LINQ to find the level by its name.
            Level level = (from lvl in new FilteredElementCollector(doc).
                           OfClass(typeof(Level)).
                           Cast<Level>()
                           where (lvl.Name == levelName)
                           select lvl).First();
            
            
            

            // Convert coordinates to double and create XYZ point.

            XYZ xyz = new XYZ(point_midle_wall_longi.X, point_midle_wall_longi.Y, level.Elevation);

            #region 

            #endregion

            // Create window.
            using (Transaction t = new Transaction(doc, "Create window"))
            {
                t.Start();

                if (!familySymbol.IsActive)
                {
                    // Ensure the family symbol is activated.
                    familySymbol.Activate();
                    doc.Regenerate();
                }

                // Create window
                // unliss you specified a host, Rebit will create the family instance as orphabt object.
                FamilyInstance window = doc.Create.NewFamilyInstance(xyz, familySymbol, wall, StructuralType.NonStructural);
                t.Commit();
            }
//            string prompt = "The element was created!";
//            TaskDialog.Show("Revit", prompt);
        }
	
		
	}
}