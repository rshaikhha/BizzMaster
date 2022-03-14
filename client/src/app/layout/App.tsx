import { ThemeProvider } from "@emotion/react";
import { Box, Container, createTheme, CssBaseline, Divider, Drawer, Grid, List, ListItem, ListItemIcon, ListItemText, Paper, Toolbar } from "@mui/material";
import { useCallback, useEffect, useState } from "react";
import { Route, Switch } from "react-router-dom";
import AboutPage from "../../features/about/AboutPage";

import HomePage from "../../features/home/HomePage";
import { useAppDispatch } from "../store/configureStore";
import Header from "./Header";
import Sidebar from "./Sidebar";
import Loadingcomponent from "./Loadingcomponent";
import { fetchCurrentUser } from "../../features/account/accountSlice";
import Countries from "../../features/system/Countries";
import Login from "../../features/account/login";
import Register from "../../features/account/Register";
import Cars from "../../features/cars/Cars";
import Categories from "../../features/system/Categories";
import Brands from "../../features/system/Brands";
import CarBrands from "../../features/cars/CarBrands";
import Platforms from "../../features/cars/Platforms";
import CarFinder from "../../features/cars/CarFinder";
import UsageTypes from "../../features/system/Usagetypes";
import MasterSystems from "../../features/system/MasterSystems";
import Products from "../../features/product/Products";
import ProductDetails from "../../features/product/ProductDetails";
import Suppliers from "../../features/supply/Suppliers";
import SupplierDetails from "../../features/supply/SupplierDetails";
import SupplyLines from "../../features/supply/SupplyLines";
import SupplyLineDetails from "../../features/supply/SupplyLineDetails";
import SubmitSalesForecast from "../../features/supply/SubmitSalesForecast";
import SalesForecast from "../../features/supply/SalesForecast";
import SalesForecastHistory from "../../features/supply/SalesForecastHistory";
import Stock from "../../features/stock/Stock";
import SubmitStock from "../../features/stock/SubmitStock";
import SubmitOrder from "../../features/order/SubmitOrder";
import Order from "../../features/order/Order";
import SupplyLineAudit from "../../features/supply/SupplyLineAudit";
import StockHistory from "../../features/stock/StockHistory";
import OrderHistory from "../../features/order/OrderHistory";
import LeadTime from "../../features/leadtime/LeadTime";
import LeadTimeHistory from "../../features/leadtime/LeadTimeHistory";
import SubmitLeadTime from "../../features/leadtime/SubmitLeadTime";
import CommercialCards from "../../features/commercial/CommercialCards";
import OrderRegistrations from "../../features/commercial/OrderRegistrations";
import CommercialCardDetail from "../../features/commercial/CommercialCardDetail";
import OrderRegistrationDetail from "../../features/commercial/OrderRegistrationDetail";
import Projects from "../../features/project/Projects";
import ProjectWizard1 from "../../features/project/ProjectWizard1";
import ProjectWizard2 from "../../features/project/ProjectWizard2";
import ProjectWizard3 from "../../features/project/ProjectWizard3";

function App() {

  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);

  const initApp = useCallback(async () => {
    try {
      await dispatch(fetchCurrentUser());
    } catch (error) {
      console.log(error);

    }
  }, [dispatch])

  useEffect(() => {
    initApp().then(() => setLoading(false));
  }, [initApp])

  const [darkMode, setDarkMode] = useState(false);
  const palletteType = darkMode ? 'dark' : 'light';
  const theme = createTheme({
    palette: {
      mode: palletteType,
      background: {
        default: palletteType === 'light' ? '#eaeaea' : '#121212'
      }
    }
  });

  function handleThemeChange() {
    setDarkMode(!darkMode);
  }

  if (loading) return <Loadingcomponent message='Initializing App'></Loadingcomponent>

  return (
    <ThemeProvider theme={theme}>
      <Box sx={{ display: 'flex' }}>

        <CssBaseline />
        <Header darkMode={darkMode} handleThemeChange={handleThemeChange} />
        <Sidebar ></Sidebar>
        <Box component="main" sx={{ flexGrow: 1, p: 3 , mt: 8}}>
          <Container>
            <Switch>
              <Route exact path='/' component={HomePage} />
              <Route path='/login' component={Login} />
              <Route path='/register' component={Register} />
              <Route path='/about' component={AboutPage} />

              <Route path='/countries' component={Countries} />
              <Route path='/brands' component={Brands} />
              <Route path='/usagetypes' component={UsageTypes} />
              <Route path='/mastersystems' component={MasterSystems} />
              <Route path='/categories' component={Categories} />

              <Route path='/cars' component={Cars} />
              <Route path='/carbrands' component={CarBrands} />
              <Route path='/platforms' component={Platforms} />
              <Route path='/carfinder' component={CarFinder} />

              <Route exact path='/products' component={Products} />
              <Route path='/products/:id' component={ProductDetails} />

              <Route exact path='/suppliers' component={Suppliers} />
              <Route path='/suppliers/:id' component={SupplierDetails} />
              <Route exact path='/supplylines' component={SupplyLines} />
              <Route path='/SupplyLineDetails/:id' component={SupplyLineDetails} />
              <Route path='/SupplyLineAudit/:id' component={SupplyLineAudit} />

              <Route exact path='/CommercialCards' component={CommercialCards} />
              <Route path='/CommercialCards/:id' component={CommercialCardDetail} />
              <Route exact path='/OrderRegistrations' component={OrderRegistrations} />
              <Route path='/OrderRegistrations/:id' component={OrderRegistrationDetail} />

              <Route path='/Leadtime/:id' component={LeadTime} />
              <Route path='/SubmitLeadTime/:id' component={SubmitLeadTime} />
              <Route path='/Leadtimehistory/:id' component={LeadTimeHistory} />


              <Route path='/SalesForecast/:id' component={SalesForecast} />
              <Route path='/SubmitSalesForecast/:id' component={SubmitSalesForecast} />
              <Route path='/SalesForecasthistory/:id/:year/:month' component={SalesForecastHistory} />

              <Route path='/stock/:id' component={Stock} />
              <Route path='/SubmitStock/:id' component={SubmitStock} />
              <Route path='/Stockhistory/:id/:year/:month' component={StockHistory} />

              <Route path='/Order/:id' component={Order} />
              <Route path='/SubmitOrder/:id' component={SubmitOrder} />
              <Route path='/Orderhistory/:id/:year/:month' component={OrderHistory} />

              <Route exact path='/Projects' component={Projects} />
              <Route path='/ProjectWizard1' component={ProjectWizard1} />
              <Route path='/ProjectWizard2/:id' component={ProjectWizard2} />
              <Route path='/ProjectWizard3/:id/:orderId' component={ProjectWizard3} />

            </Switch>
          </Container>
        </Box>
      </Box>
    </ThemeProvider>
  );
}

export default App;
