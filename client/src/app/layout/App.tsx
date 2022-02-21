import { ThemeProvider } from "@emotion/react";
import { Container, createTheme, CssBaseline } from "@mui/material";
import { useCallback, useEffect, useState } from "react";
import { Route, Switch } from "react-router-dom";
import AboutPage from "../../features/about/AboutPage";

import HomePage from "../../features/home/HomePage";
import { useAppDispatch } from "../store/configureStore";
import Header from "./Header";
import Loadingcomponent from "./Loadingcomponent";
import { fetchCurrentUser } from "../../features/account/accountSlice";

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

  useEffect(()=>{
    initApp().then(()=> setLoading(false));
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

  function handleThemeChange(){
    setDarkMode(!darkMode);
  }

  if (loading) return <Loadingcomponent message= 'Initializing App'></Loadingcomponent>

  return (
    <ThemeProvider theme= {theme}>
      <CssBaseline />
      <Header darkMode= {darkMode} handleThemeChange={handleThemeChange}/>
      <Container>
        <Switch>
          <Route exact path='/' component={HomePage} />
          <Route path='/about' component={AboutPage} />
        </Switch>
      </Container>
    </ThemeProvider>
  );
}

export default App;
