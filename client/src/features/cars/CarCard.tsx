import { Card, CardMedia, CardContent, Typography, CardActions, Button, Avatar, CardHeader, Collapse, IconButton, styled } from "@mui/material";
import { red } from "@mui/material/colors";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import { Car } from "../../app/models/car";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import { useState } from "react";

interface Props {
    data: Car
}



export default function CarCard(props: Props) {
    const [open, setOpen] = useState(false);

    if (props.data == null) return <Loadingcomponent />

    return (
        <Card sx={{ maxWidth: '80%' }}>
      <CardHeader
        avatar={
          <Avatar sx={{ bgcolor: red[500] }} aria-label="recipe" src={props.data.brandLogoImage} />
        }
        action={
          <IconButton aria-label="settings">
            {/* <MoreVertIcon /> */}
          </IconButton>
        }
        title={props.data.title}
        subheader={props.data.brandTitle + ' ' + props.data.platformTitle}
      />
      <CardMedia
        component="img"
        height="194"
        image='Images/SampleCar.jpg'
        alt={props.data.title}
      />
      <CardContent>
        <Typography variant="body2" color="text.secondary">
        The Toyota Camry is an automobile sold internationally by the Japanese manufacturer Toyota since 1982, spanning multiple generations.
        </Typography>
      </CardContent>
      <CardActions disableSpacing>
          <Button fullWidth onClick={()=>setOpen(!open)} >
          <Typography variant='h6'>Details</Typography>
        {open  ? <ExpandLess /> : <ExpandMore  />}
          </Button>
        
      </CardActions>
      <Collapse in={open} timeout="auto" unmountOnExit>
        <CardContent>
          <Typography variant='h5'>Caoacity:</Typography>
          <Typography paragraph>
            Engine Oil Capacity: 3.5 Lit
            Gearbox Capacity: 8 Lit
          </Typography>
          <Typography paragraph>
            Heat oil in a (14- to 16-inch) paella pan or a large, deep skillet over
            medium-high heat. Add chicken, shrimp and chorizo, and cook, stirring
            occasionally until lightly browned, 6 to 8 minutes. Transfer shrimp to a
            large plate and set aside, leaving chicken and chorizo in the pan. Add
            piment??n, bay leaves, garlic, tomatoes, onion, salt and pepper, and cook,
            stirring often until thickened and fragrant, about 10 minutes. Add
            saffron broth and remaining 4 1/2 cups chicken broth; bring to a boil.
          </Typography>
          <Typography paragraph>
            Add rice and stir very gently to distribute. Top with artichokes and
            peppers, and cook without stirring, until most of the liquid is absorbed,
            15 to 18 minutes. Reduce heat to medium-low, add reserved shrimp and
            mussels, tucking them down into the rice, and cook again without
            stirring, until mussels have opened and rice is just tender, 5 to 7
            minutes more. (Discard any mussels that don???t open.)
          </Typography>
          <Typography>
            Set aside off of the heat to let rest for 10 minutes, and then serve.
          </Typography>
        </CardContent>
      </Collapse>
    </Card >
    )

}