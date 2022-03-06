import { ExpandLess, ExpandMore, StarBorder } from "@mui/icons-material";
import { List, ListSubheader, ListItemButton, ListItemIcon, ListItemText, Collapse, ListItem, Skeleton, CircularProgress, Box, AppBar, CssBaseline, Divider, Drawer, Toolbar, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import { Brand, Car, Platform } from "../../app/models/car"
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';

interface Props {
  title: string;
  items: { header: string, accessor: string }[],
}



export default function CarsOld() {
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

  const drawerWidth = 240;

  return (
    
    <Box>
      <Typography paragraph>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
        tempor incididunt ut labore et dolore magna aliqua. Rhoncus dolor purus non
        enim praesent elementum facilisis leo vel. Risus at ultrices mi tempus
        imperdiet. Semper risus in hendrerit gravida rutrum quisque non tellus.
        Convallis convallis tellus id interdum velit laoreet id donec ultrices.
        Odio morbi quis commodo odio aenean sed adipiscing. Amet nisl suscipit
        adipiscing bibendum est ultricies integer quis. Cursus euismod quis viverra
        nibh cras. Metus vulputate eu scelerisque felis imperdiet proin fermentum
        leo. Mauris commodo quis imperdiet massa tincidunt. Cras tincidunt lobortis
        feugiat vivamus at augue. At augue eget arcu dictum varius duis at
        consectetur lorem. Velit sed ullamcorper morbi tincidunt. Lorem donec massa
        sapien faucibus et molestie ac.
      </Typography>
      <Typography paragraph>
        Consequat mauris nunc congue nisi vitae suscipit. Fringilla est ullamcorper
        eget nulla facilisi etiam dignissim diam. Pulvinar elementum integer enim
        neque volutpat ac tincidunt. Ornare suspendisse sed nisi lacus sed viverra
        tellus. Purus sit amet volutpat consequat mauris. Elementum eu facilisis
        sed odio morbi. Euismod lacinia at quis risus sed vulputate odio. Morbi
        tincidunt ornare massa eget egestas purus viverra accumsan in. In hendrerit
        gravida rutrum quisque non tellus orci ac. Pellentesque nec nam aliquam sem
        et tortor. Habitant morbi tristique senectus et. Adipiscing elit duis
        tristique sollicitudin nibh sit. Ornare aenean euismod elementum nisi quis
        eleifend. Commodo viverra maecenas accumsan lacus vel facilisis. Nulla
        posuere sollicitudin aliquam ultrices sagittis orci a.
      </Typography>
    </Box>
    
  )
}