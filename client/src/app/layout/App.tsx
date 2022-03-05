import { ThemeProvider } from "@emotion/react";
import { Box, Container, createTheme, CssBaseline, Divider, Drawer, Grid, List, ListItem, ListItemIcon, ListItemText, Paper, Toolbar } from "@mui/material";
import { useCallback, useEffect, useState } from "react";
import { Route, Switch } from "react-router-dom";
import AboutPage from "../../features/about/AboutPage";

import HomePage from "../../features/home/HomePage";
import { useAppDispatch } from "../store/configureStore";
import Header from "./Header";
import Loadingcomponent from "./Loadingcomponent";
import { fetchCurrentUser } from "../../features/account/accountSlice";
import Countries from "../../features/basics/Countries";
import VehicleBrands from "../../features/basics/Brands";
import Basics from "../../features/basics/Basics";
import Login from "../../features/account/login";
import Register from "../../features/account/Register";
import Cars from "../../features/cars/Cars";

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
      <CssBaseline />
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange} />
      <Container>
        <Switch>
          <Route exact path='/' component={HomePage} />
          <Route path='/login' component={Login} />
          <Route path='/register' component={Register} />
          <Route path='/about' component={AboutPage} />
          <Route path='/basics' component={Basics} />
          <Route path='/cars' component={Cars} />
        </Switch>
      </Container>
    </ThemeProvider>
  );
}

export default App;
