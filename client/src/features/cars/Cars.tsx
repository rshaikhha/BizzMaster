import { ExpandLess, ExpandMore, StarBorder } from "@mui/icons-material";
import { List, ListSubheader, ListItemButton, ListItemIcon, ListItemText, Collapse, ListItem, Skeleton, CircularProgress } from "@mui/material";
import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import { Brand, Car, Platform } from "../../app/models/car"

interface Props {
  title: string;
  items: { header: string, accessor: string }[],
}



export default function Cars() {
  const [brands, setBrands] = useState<Brand[]>([]);
  const [platforms, setPlatforms] = useState<Platform[]>([]);
  const [cars, setCars] = useState<Car[]>([]);

  const [openBrand, setOpenBrand] = useState<string>('')


  useEffect(() => {
    agent.Cars.brands().then((res) => setBrands(res));
    
  }, [])

  function handleBrandClick(brandTitle: string) {
    setPlatforms([]);
    if(openBrand !== brandTitle) {setOpenBrand(brandTitle)} else {setOpenBrand('')};
    agent.Cars.platforms(brandTitle).then((res) => setPlatforms(res));
  }

  function handlePlatformClick(item: Platform) {
    if (!item.isOpen) {
      const platformTitle = item.title;
      agent.Cars.cars(platformTitle).then((res) => setCars(res));
    }
    item.isOpen = !item.isOpen;

  }



  return (

    <List
      sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}
      component="nav"
      aria-labelledby="nested-list-subheader"
      subheader={
        <ListSubheader component="div" id="nested-list-subheader">
          Cars {openBrand}
        </ListSubheader>
      }
    >
      {brands.map((item) => (
        <>
          <ListItemButton key={item.title} onClick={() => handleBrandClick(item.title)}>
            <ListItemText primary={item.title} />
            {item.title === openBrand ? <ExpandLess /> : <ExpandMore />}
          </ListItemButton>
            
              <Collapse in= {item.title === openBrand} timeout = "auto" unmountOnExit>
              <List component="div" disablePadding>
                {platforms.length == 0 ? (

                  [1,2,3].map((_,index) => (

                    <ListItemButton key={index} sx={{ paddingLeft: 6 }}>
                      <ListItemIcon>
                      <CircularProgress
        variant="indeterminate"
        disableShrink
        sx={{
          color: '#1a90ff',
          animationDuration: '550ms',
          //position: 'absolute',
          //left: 0,
        }}
        size={25}
        thickness={4}
      />
                      </ListItemIcon>
                    <ListItemText primary="...Loading!" >
                    <ExpandMore />
                    </ListItemText>
                    </ListItemButton>
                  ))

              ) : (
                platforms.map((pf) => (
                  <>
                    <ListItemButton key={pf.title} sx={{ pl: 6 }} onClick={() => handlePlatformClick(pf)}>
                      <ListItemText primary={pf.title} />
                      {openBrand === pf.title ? <ExpandLess /> : <ExpandMore />}
                    </ListItemButton>
                    <Collapse in={openBrand === pf.title} timeout="auto" unmountOnExit>

                      <List component="div" disablePadding>
                        {cars.map((car) => (
                          <>
                            <ListItemButton key={car.title} sx={{ pl: 12 }} >
                              <ListItemText primary={car.title} />
                            </ListItemButton>
                          </>
                        ))}

                      </List>
                    </Collapse>
                  </>
                ))

              )}
              </List>
              </Collapse>
            
          {/* <Collapse in={item.isOpen} timeout="auto" unmountOnExit>
            <Skeleton animation="wave" />
            <Skeleton animation="wave" />
            <Skeleton animation="wave" />
            <List component="div" disablePadding>
              {platforms.map((pf) => (
                <>
                  <ListItemButton key={pf.title} sx={{ pl: 6 }} onClick={() => handlePlatformClick(pf)}>
                    <ListItemText primary={pf.title} />
                    {openBrand === pf.title ? <ExpandLess /> : <ExpandMore />}
                  </ListItemButton>
                  <Collapse in={openBrand === pf.title} timeout="auto" unmountOnExit>

                    <List component="div" disablePadding>
                      {cars.map((car) => (
                        <>
                          <ListItemButton key={car.title} sx={{ pl: 12 }} >
                            <ListItemText primary={car.title} />
                          </ListItemButton>
                        </>
                      ))}

                    </List>
                  </Collapse>
                </>
              ))}

            </List>
          </Collapse> */}
        </>
      ))}




    </List>
  );
}