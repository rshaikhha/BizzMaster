import { Container, Box, Avatar, Typography, TextField, Button } from "@mui/material";
import { useForm, FieldValues } from "react-hook-form";
import { Redirect, useHistory } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import AddReactionIcon from '@mui/icons-material/AddReaction';
import agent from "../../app/api/agent";


export default function Invite() {
    const history = useHistory();
    const dispatch = useAppDispatch();
    
    const {user} = useAppSelector(state => state.account);


    const {register, handleSubmit,setError, formState: {isSubmitting, errors, isValid}} = useForm({
        mode: 'all'
    });

    async function submitForm(data: FieldValues) {
        
        const result = agent.Account.invite(data)
            .then(res => history.push('/dashboard'))
            .catch(errors => {
                errors.forEach((error: string) => {
                    setError('username', { message: error })
                });
            });
    }

    if(!user?.token) return <Redirect push to="/login" />
    if(!user?.roles.includes('CanInvite')) return <Redirect push to="/dashboard" />
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
                        <AddReactionIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        دعوت از همکار 
                    </Typography>
                    <Box 
                    component="form" 
                    onSubmit={handleSubmit(submitForm)} noValidate sx={{ mt: 1 }}>
                        <TextField
                            margin="normal"
                            fullWidth
                            label="Phone Number"
                            autoFocus
                            {...register('username', {
                                required: 'please enter your phone number',
                                pattern: {
                                    value: /^09\d{9}$/,
                                    message: 'Not Valid'
                                }
                            })}
                            error={!!errors.username}
                            helperText = {errors?.username?.message}
                        />
                        {/* <TextField
                            margin="normal"
                            fullWidth
                            label="Password"
                            type="password"
                            {...register('password', {required: 'password is required'})}
                            error={!!errors.password}
                            helperText = {errors?.password?.message}
                        /> */}
                        <Button
                            disabled = {isSubmitting || !isValid}
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            ارسال
                        </Button>
                    </Box>
                </Box>

            </Container>
    );
}


