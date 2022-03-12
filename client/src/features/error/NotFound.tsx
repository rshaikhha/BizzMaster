import { Button, Container, Paper, Typography } from "@mui/material";
import { Link } from "react-router-dom";

export default function NotFound() {
    return (
        <Container component={Paper} sx={{ height: 200 }}>
            <Typography gutterBottom variant="h3">
                OOPS WE COULD NOT FIND THIS PAGE
            </Typography>
            <Button fullWidth component={Link} to='/'>
                Go Back To Home
            </Button>
        </Container>
    )
}