import { Box, Pagination, Typography } from "@mui/material";
import { MetaData } from "../models/pagination";

interface Props {
    metaData: MetaData;
    onPageChange: (page: number) => void;
}


export default function AppPagination(props: Props){
    const {currentPage,totalCount, totalPages, pageSize } = props.metaData;
    return (
        <Box display= 'flex' justifyContent='space-between' alignItems='center'>
                        <Typography>
                            Displaying 
                            {(currentPage-1)*pageSize + 1}-
                            {currentPage*pageSize > totalCount
                                ? totalCount
                                : currentPage*pageSize} of {totalCount} Items
                        </Typography>
                        <Pagination 
                            color= 'secondary' 
                            size='large'
                            count={totalPages}
                            page={currentPage}
                            onChange= {(e, page) => props.onPageChange(page)}

                            />
                    </Box>
    )
}