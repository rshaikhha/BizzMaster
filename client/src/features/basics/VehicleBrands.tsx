import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import SimpleTable from "../shared/SimpleTable";
export default function VehicleBrands() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.Vehicles.brands().then((res) => setList(res))
    }, [])

    const title = 'Vehicle Brands';
    const columns = [
        { header: 'Name', accessor: 'title'},
        { header: 'Country', accessor: 'countryName'}

    ];

    const props = {list, title, columns}
    return (
        <SimpleTable {...props} />

    )


}