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

              <Route path='/products' component={Products} />
            </Switch>
          </Container>
        </Box>
      </Box>
    </ThemeProvider>
  );
}

export default App;
