<#
.SYNOPSIS
scans a csv for computernames or usernames
.DESCRIPTION
scans the set csv to get either the computernames or usernames using the input ip range to filter
.PARAMETER GetUsers
if set, will get the users assigned to computers within the range
.PARAMETER AddressRangeInput
PipelineInput, gets 2 ip's seperated by the '-' character and filters the csv using their final digits, as such everything but the last 3 digits can be left out
.PARAMETER ComputerListPath
the path to the csv which is searched through, defaults to "./ComputerList.csv"
.EXAMPLE
scanComputerList "192.168.0.000 - 192.168.0.031"
.EXAMPLE
scanComputerList "0 - 31"
#>
param (
    # set if users or hostnames are returned
    [switch]$GetUsers,
    
    # address Range
    [Parameter(Mandatory=$true,ValueFromPipeline=$true)]
    [string]
    $AddressRangeInput,
    
    # Path to Computer List
    [Parameter(Mandatory=$false)]
    [System.IO.FileInfo]
    $ComputerListPath="./ComputerList.csv"
)

#powershell wont decide to output non-empty variables in classes
class PleaseDontOutput {
    [System.Collections.Generic.List[string]] static GetComputerNames([int]$Start,[int]$End,$ComputerListPath){
        [System.Collections.Generic.List[string]] $Out = [System.Collections.Generic.List[string]]::new();
        $Computers = Import-Csv $ComputerListPath;
        for ($i = 0; $i -lt $Computers.Count; $i++) {
            $CompID = [int]($Computers[$i].IPAddress.Split('.')[3]);
            if ($CompID -ge $Start -and $CompID -le $End) {
                $Out.Add($Computers[$i].ComputerName);
            }
        }
        return $out;
    }
    [System.Collections.Generic.List[string]] static GetUserNames([int]$Start,[int]$End,$ComputerListPath){

        [System.Collections.Generic.List[string]] $Out = [System.Collections.Generic.List[string]]::new();
        $Computers = Import-Csv $ComputerListPath;
        for ($i = 0; $i -lt $Computers.Count; $i++) {
            $CompID = [int]($Computers[$i].IPAddress.Split('.')[3]);
            if ($CompID -ge $Start -and $CompID -le $End) {
                foreach ($Name in $Computers[$i].Username.Split(' ')) {
                    if (!$Out.Contains($Name)) {
                        $Out.Add($Name);
                    }
                }
            }
        }
        return $out;
    }
}


$tmp = $AddressRangeInput.Split('-');
$tmp2 = $tmp[0].Trim(' ').Split('.');
$StartAddress = $tmp2[$tmp2.Count-1];
$tmp = $tmp[1].Trim(' ').Split('.');
$EndAddress = $tmp[$tmp.Count-1];
$tmp = $null;
$tmp2 = $null;
if ($GetUsers) {
    $result = [PleaseDontOutput]::GetUserNames($StartAddress,$EndAddress,$ComputerListPath)
}
else {
    $result = [PleaseDontOutput]::GetComputerNames($StartAddress,$EndAddress,$ComputerListPath)
}
for ($i = 0; $i -lt $result.Count; $i++) {
    Write-Output $result[$i];
}
$result = $null;
$StartAddress = $null;
$EndAddress = $null;