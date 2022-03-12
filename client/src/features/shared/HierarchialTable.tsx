import { ExpandLess, ExpandMore } from "@mui/icons-material";
import { CircularProgress, Collapse, List, ListItemButton, ListItemIcon, ListItemText, ListSubheader } from "@mui/material";
import { useState } from "react";
import SimpleTable from "./SimpleTable";

interface Props {
    title: string;
    list: any[];

}

export default function HierarchialTable(props: Props) {
    const [open, setOpen] = useState<string>('')
    const [tp, setTp] = useState<any>(null)
    const title = props.title;
    const list = props.list;

    const handleClick = (item: any) => {
        const itemkey = item.key
        if (open !== itemkey) { setOpen(itemkey) } else { setOpen('') };

        setTp(
            {list :[... item], title : "Properties", columns : [
                { header: 'title', accessor: 'title'},
                { header: 'Usage Type', accessor: 'usageType'},
                { header: 'Master System', accessor: 'masterSystem'},
                { header: 'HSCode', accessor: 'hscode'}
        
            ] }

        )

    }


    return (
        <>
            <List
                sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}
                component="nav"
                aria-labelledby="nested-list-subheader"
                subheader={
                    <ListSubheader component="div" id="nested-list-subheader">
                        {title}
                    </ListSubheader>
                }
            >
                {list.map((item) => (
                    <>
                        <ListItemButton sx={{ pl: 4 * (1 + item.level) }} key={item.title} onClick={() => { item.children?.length > 0 ? handleClick(item.title) : handleClick('') }}>
                            <ListItemText primary={item.title} />
                            {item.children?.length > 0 && (item.title === open ? <ExpandLess /> : <ExpandMore />)}
                        </ListItemButton>
                        <Collapse in={item.title === open} timeout="auto" unmountOnExit>
                            <List component="div" disablePadding>
                                <HierarchialTable {... { title: "", list: item.children }} />
                            </List>
                        </Collapse>

                    </>
                ))}

            </List >
        </>
    )
}