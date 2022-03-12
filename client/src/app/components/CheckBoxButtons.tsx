import { Checkbox, FormControlLabel, FormGroup, FormLabel, Typography } from "@mui/material";
import { useState } from "react";

interface Props {
    title: string;
    items: any[];
    checked?: string[];
    onChange: (items: any[]) => void;
}

export default function CheckBoxButtons(props: Props) {

    const [checkedItems, setCheckedItems] = useState(props.checked || [])

    function handleChecked(value: string) {
        const currentIndex = checkedItems.findIndex(item => item === value)
        let newChecked : string[] = [];
        if (currentIndex === -1) newChecked = [...checkedItems, value];
        else newChecked = checkedItems.filter(item => item !== value);
        setCheckedItems(newChecked);
        props.onChange(newChecked);
    }

    return (
        <FormGroup>
            <FormLabel component="legend">{props.title}</FormLabel>
            {props.items.map((item) => (
                <FormControlLabel 
                    control={<Checkbox
                        sx={{'& .MuiSvgIcon-root': {fontSize: 18}}}
                        checked = {checkedItems.indexOf(item.id) !== -1} />} 
                        onClick={() => handleChecked(item.id)}
                        label={<Typography variant="body2" color="textSecondary">{item.title}</Typography>}
                        key={item.id} />
            ))}
        </FormGroup>
    )
}