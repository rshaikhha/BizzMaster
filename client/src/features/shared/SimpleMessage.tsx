import { Container, Box, Avatar, Typography, TextField, Button } from "@mui/material";
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';


interface Props {
    message: string;
}

const defaultProps = {
    message: 'You are not allowed to visit this page!'
}

export default function SimpleMessage(props: Props = defaultProps) {

    return (
            <Container component="main" maxWidth="xs">
                <Box
                    sx={{
                        marginTop: 8,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}
                >
                    <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        {props.message} 
                    </Typography>
                    {/* <Box 
                    component="form" 
                    onSubmit={handleSubmit(submitForm)} noValidate sx={{ mt: 1 }}>
                        <TextField
                            disabled = {user!==null}
                            margin="normal"
                            fullWidth
                            label="Username"
                            autoFocus
                            {...register('username', {required: 'username is required'})}
                            error={!!errors.username}
                            helperText = {errors?.username?.message}
                        />
                        <TextField
                            disabled = {user!==null}
                            margin="normal"
                            fullWidth
                            label="Password"
                            type="password"
                            {...register('password', {required: 'password is required'})}
                            error={!!errors.password}
                            helperText = {errors?.password?.message}
                        />
                        <Button
                            disabled = {isSubmitting || !isValid || user!==null}
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            SUBMIT
                        </Button>
                    </Box> */}
                </Box>

            </Container>
    );
}