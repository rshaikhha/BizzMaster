import { Box, Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import TableRowsIcon from '@mui/icons-material/TableRows';
import { NavLink } from "react-router-dom";
import ReadMoreIcon from '@mui/icons-material/ReadMore';

interface Props {
    list : any[],
    title : string,
    columns : {header : string, accessor: string} [],
    startIndex? : number,
    detailsAddress? : string
}

export default function SimpleTable(props : Props) {

    const {list, title, columns} = props;
    
    return (
        <>
        <Paper sx={{ padding: 2 }}>

        <Typography variant='h4'> <TableRowsIcon fontSize="inherit" style={{verticalAlign:"middle"}} /> {title}</Typography>

        <TableContainer >
            <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                <TableHead
                sx ={{bgcolor: 'grey.300'}}
                >
                    <TableRow>
                        <TableCell key="index">Index</TableCell>
                        {columns.map((item) => <TableCell key={item.header}>{item.header}</TableCell>)}
                        <TableCell key="index">Details</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {list.map((row, index) => (
                        <TableRow
                            key={index}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 }, '&:hover': { backgroundColor: 'grey.200' }, textDecoration: 'none' } }
                        >
                            <TableCell>{(props.startIndex || 1) + index}</TableCell>
                            {columns.map((item, itemIndex) => (
                                <TableCell key={itemIndex}>{row[item.accessor]}</TableCell>
                            ))}
                            <TableCell key='link'><Button component={NavLink} to={`/${props.detailsAddress}/${row.id}`}><ReadMoreIcon /></Button></TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
        </Paper>
        </>

    )


}