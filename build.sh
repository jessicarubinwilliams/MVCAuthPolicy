#!/bin/bash
function logo { #logo for build program
echo '          _____                   _____                   _____                   _____    _____                   _____                   _____          '
echo '         /\    \                 /\    \                 /\    \                 /\    \  /\    \                 /\    \                 /\    \         '
echo '        /::\    \               /::\____\               /::\    \               /::\____\/::\    \               /::\    \               /::\    \        '
echo '       /::::\    \             /:::/    /               \:::\    \             /:::/    /::::\    \             /::::\    \             /::::\    \       '
echo '      /::::::\    \           /:::/    /                 \:::\    \           /:::/    /::::::\    \           /::::::\    \           /::::::\    \      '
echo '     /:::/\:::\    \         /:::/    /                   \:::\    \         /:::/    /:::/\:::\    \         /:::/\:::\    \         /:::/\:::\    \     '
echo '    /:::/__\:::\    \       /:::/    /                     \:::\    \       /:::/    /:::/  \:::\    \       /:::/__\:::\    \       /:::/__\:::\    \    '
echo '   /::::\   \:::\    \     /:::/    /                      /::::\    \     /:::/    /:::/    \:::\    \     /::::\   \:::\    \     /::::\   \:::\    \   '
echo '  /::::::\   \:::\    \   /:::/    /      _____   ____    /::::::\    \   /:::/    /:::/    / \:::\    \   /::::::\   \:::\    \   /::::::\   \:::\    \  '
echo ' /:::/\:::\   \:::\ ___\ /:::/____/      /\    \ /\   \  /:::/\:::\    \ /:::/    /:::/    /   \:::\ ___\ /:::/\:::\   \:::\    \ /:::/\:::\   \:::\____\ '
echo '/:::/__\:::\   \:::|    |:::|    /      /::\____/::\   \/:::/  \:::\____/:::/____/:::/____/     \:::|    /:::/__\:::\   \:::\____/:::/  \:::\   \:::|    |'
echo '\:::\   \:::\  /:::|____|:::|____\     /:::/    \:::\  /:::/    \::/    \:::\    \:::\    \     /:::|____\:::\   \:::\   \::/    \::/   |::::\  /:::|____|'
echo ' \:::\   \:::\/:::/    / \:::\    \   /:::/    / \:::\/:::/    / \/____/ \:::\    \:::\    \   /:::/    / \:::\   \:::\   \/____/ \/____|:::::\/:::/    / '
echo '  \:::\   \::::::/    /   \:::\    \ /:::/    /   \::::::/    /           \:::\    \:::\    \ /:::/    /   \:::\   \:::\    \           |:::::::::/    /  '
echo '   \:::\   \::::/    /     \:::\    /:::/    /     \::::/____/             \:::\    \:::\    /:::/    /     \:::\   \:::\____\          |::|\::::/    /   '
echo '    \:::\  /:::/    /       \:::\__/:::/    /       \:::\    \              \:::\    \:::\  /:::/    /       \:::\   \::/    /          |::| \::/____/    '
echo '     \:::\/:::/    /         \::::::::/    /         \:::\    \              \:::\    \:::\/:::/    /         \:::\   \/____/           |::|  ~|          '
echo '      \::::::/    /           \::::::/    /           \:::\    \              \:::\    \::::::/    /           \:::\    \               |::|   |          '
echo '       \::::/    /             \::::/    /             \:::\____\              \:::\____\::::/    /             \:::\____\              \::|   |          '
echo '        \::/    /               \::/    /               \::/    /               \::/    /\::/    /               \::/    /               \:|   |          '
echo '         \/____/                 \/____/                 \/____/                 \/____/  \/____/                 \/____/                 \|___|          '                                                    
echo ''
}

function startup { #startup routine & questionare
clear
logo;
echo "Enter the desired project name: 🏠";
read project_name;
clear
logo;
echo "Enter the desired name for Foo (Category): 👤";
read foo;
clear
logo;
echo "Enter the desired name for Bar (Item): 👥";
read bar;
clear
logo;
echo "Enter the desired SQL database name: (You need to create the schema beforehand.) 🗂";
read sql;
clear
logo;
echo "Here are your desired replacements:"
echo '';
echo "🏠 Project Name: $project_name";
echo '';
echo "👤 Foo (Category): $foo";
echo '';
echo "👥 Bar (Item): $bar";
echo '';
echo "🗂  SQL database name: $sql";

# check if user responses are valid
if [ -z "$project_name" ] | [ -z "$foo" ] | [ -z "$bar" ] | [ -z "$sql" ]; #check if response from user is valid
then
startup;
else 
echo ''
read -p "Continue (y/n)? " CONT
if [ "$CONT" = "y" ]; then

# change directory names
clear
find . -depth -type d -name '*MyProject*'  -execdir bash -c 'mv "$1" "${1/MyProject/'"$project_name"'}"' -- {} \;> /dev/null 2>&1
echo "✅ Replace Project Name Directories."
find . -depth -type d -name '*Foo*'  -execdir bash -c 'mv "$1" "${1/Foo/'"$foo"'}"' -- {} \;> /dev/null 2>&1
echo "✅ Replace Foo Directory Occurences."
find . -depth -type d -name '*Bar*'  -execdir bash -c 'mv "$1" "${1/Bar/'"$bar"'}"' -- {} \;> /dev/null 2>&1
echo "✅ Replace Bar Directory Occurences."

#change occurences in files
find . -type f ! -name "*.sh" -exec sed -i'' -e "s/MyProject/$project_name/g" {} +> /dev/null 2>&1
echo "✅ Replace Project Namespaces."
find . -type f ! -name "*.sh" -exec sed -i'' -e "s/Foo/$foo/g" {} +> /dev/null 2>&1
echo "✅ Replace Foo String occurences."
find . -type f ! -name "*.sh" -exec sed -i'' -e "s/Bar/$bar/g" {} +> /dev/null 2>&1
echo "✅ Replace Bar String occurences."
find . -type f ! -name "*.sh" -exec sed -i'' -e "s/SQLNAMEHERE/$sql/g" {} +> /dev/null 2>&1
echo "✅ Change SQL DB Data."

# change file names
for file in `find . -type f -name '*MyProject*'`; do mv -v "$file" "${file/MyProject/$project_name}"; done> /dev/null 2>&1
echo "✅ Change Project Name File Occurences."
for file in `find . -type f -name '*Foo*'`; do mv -v "$file" "${file/Foo/$foo}"; done> /dev/null 2>&1
echo "✅ Change Foo File Occurences."
for file in `find . -type f -name '*Bar*'`; do mv -v "$file" "${file/Bar/$bar}"; done> /dev/null 2>&1
echo "✅ Change Bar File Occurences."

# start the project
cd $project_name> /dev/null 2>&1
dotnet ef migrations add Initial> /dev/null 2>&1
echo "✅ Add Initial EF Migration."
dotnet ef database update> /dev/null 2>&1
echo "✅ EF Update Database. Running Web Server."
clear && dotnet run
else
startup;
fi
clear
fi
clear
}
startup; #actually run the startup function