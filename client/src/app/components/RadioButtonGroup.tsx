import { FormControl, FormControlLabel, FormLabel, Radio, RadioGroup, Typography } from "@mui/material";

interface Props {
    options: any[];
    onChange: (event: any) => void;
    selectedValue: string;
    title: string;
}

export default function RadioButtonGroup(props: Props) {

    return (

        <FormControl component="fieldset">
            <FormLabel component="legend">{props.title}</FormLabel>
            <RadioGroup
            
                value={props.selectedValue}
                onChange={props.onChange}
            >
                {props.options.map(({ value, label }) => {
                    return (
                        <FormControlLabel
                            key ={value}
                            value={value}
                            control={<Radio sx={{'& .MuiSvgIcon-root': {fontSize: 18}}}/>}
                            label={<Typography variant="body2" color="textSecondary">{label}</Typography>}
                        />
                    );
                })}
            </RadioGroup>
        </FormControl>
    )
}