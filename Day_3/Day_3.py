import math
from decimal import *

def main():
    print ("Spiral memory puzzle")
    puzzleInput = int(input ("Input puzzle input (integer): "))
    # First task
    distance = int(findDistance(puzzleInput))
    print ("Distance to access point for address ",puzzleInput," is ",distance)
    
    # Second task
    value = int(findFirstLargerThan(puzzleInput))
    print("First value larger than ",puzzleInput," is ",value)
    input("Hit enter to quit")


def findDistance(_address):
    """In a spiral data structure, returns the number of steps needed to get to the first index
        of the data structure. Diagonal movement not permitted."""
    x = _getCoordinateByAddress(_address, True)
    y = _getCoordinateByAddress(_address, False)
    return math.fabs(x) + math.fabs(y)

def findFirstLargerThan(_value):
    """Populates a spiral list with values until a value larger than the input is reached.
        Each address is given a value equal to the sum of its neighbours. Empty neighbours count as 0."""
    values = [0,1]
    value = values[1]
    while (value < _value and len(values) < 100):
        value = _findNextValue(len(values),values)
        values.append(value)
    return value

def _findNextValue(_address, _values):
    """For the given address, checks the neighbours populated in values list and returns their sum."""
    myX = _getCoordinateByAddress(_address,True)
    myY = _getCoordinateByAddress(_address,False)
    newValue = 0
    for x in range (-1,2):
        for y in range (-1,2):
            if x == y and x == 0:
                continue
            checkAddress = int(_getAddressByCoordinates(myX+x,myY+y))
            if checkAddress < len(_values):
                newValue = newValue + _values[checkAddress]
    return newValue

def _getCoordinateByAddress(_address, _isX):
    """Returns the x or y coordinate component of the grid corresponding
        to the address provided. Flag determines if X or Y is returned."""
    # Find out which layer we are in, and where we are in that layer.
    layerIndex = _getLayerIndex(_address)
    indexInLayer = _getIndexInLayer(_address)

    # Find the length of the sides in the layer, and which side we are on.
    sideLength = _getSideLengthByLayerIndex(layerIndex)
    sideIndex = _getSideIndexFromIndexAndLength(indexInLayer,sideLength)

    # Are we on a horizontal side?
    addressIsX = bool(int(sideIndex%2) == 1)
    
    if addressIsX == _isX :
        # We are looking for the coordinate component parallel to this side.
        delta = _getDeltaFromCenterPointOfSide(layerIndex,indexInLayer)
        return delta if (sideIndex == 0 or sideIndex == 3) else -delta
    else:
        # We are looking for the coordinate component orthogonal to this side.
        positive = bool (_getSideIndexFromIndexAndLength(indexInLayer,sideLength) < 2)
        return layerIndex if  positive else -layerIndex

def _getAddressByCoordinates(_x,_y):
    """Returns the address equivalent to the coordinates provided."""
    # Find out if we are on a horizontal side.
    sideIsX = (math.fabs(_x) >= math.fabs(_y))
    
    # Find out if we are top/right or bottom/left.
    layerComponentIsPositive = _x >= 0 if sideIsX else _y >= 0
    
    # Find out which layer we are on.
    layerIndex = math.fabs(_x) if sideIsX else math.fabs(_y)

    # Find our offset from the center of our side.
    deltaFromCenter = _y if sideIsX else _x

    # Resolve which side we are on and the lengths of this layer's sides.
    sideIndex = _getSideIndexFromLayerSignAndAxis(layerComponentIsPositive,sideIsX)
    sideLength = _getSideLengthByLayerIndex(layerIndex)

    # Invert delta if we are on the right/bottom.
    if sideIndex == 0 or sideIndex == 3:
        deltaFromCenter = -deltaFromCenter

    # Find our index witin the current side and layer.
    indexInSide = _getMidPointByLayerIndex(layerIndex) - deltaFromCenter
    indexInLayer = indexInSide + sideIndex * (sideLength - 1)

    # The last index in the layer resolves to 0, so correct the calculated index.
    if indexInLayer == 0:
        indexInLayer = (sideLength-1)*4
    return _getAreaByLayer(layerIndex - 1) + indexInLayer


# Address based functions

def _getLayerIndex(_address):
    """Gets the index of the layer the address is located in."""
    return float(Decimal(math.sqrt(_address - 1)/2).quantize(Decimal('1'), rounding = ROUND_HALF_UP))

def _getIndexInLayer(_address):
    """Gets the index of the address within its layer."""
    return (_address) - _getAreaByLayer(_getLayerIndex(_address) - 1)


# Layer based functions

def _getAreaByLayer(_layerIndex):
    """Gets the area covered by a layer."""
    return math.pow(_getSideLengthByLayerIndex(_layerIndex),2)

def _getMidPointByLayerIndex(_layerIndex):
    """Given a layer index, return the lengths of its sides."""
    sideLength = _getSideLengthByLayerIndex(_layerIndex)
    return (sideLength - 1)/2

def _getSideLengthByLayerIndex(_layerIndex):
    """Gets the length of the sides by layer."""
    return (_layerIndex * 2) + 1

def _getDeltaFromCenterPointOfSide(_layerIndex,_indexInLayer):
    """Gets the distance away from a center point along the side of a layer."""
    sideLength = _getSideLengthByLayerIndex(_layerIndex)
    midPoint = _getMidPointByLayerIndex(_layerIndex)
    indexInSide = _getIndexInSide(_indexInLayer,sideLength)
    return int(indexInSide - midPoint)


# Side functions

def _getIndexInSide(_indexInLayer,_sideLength):
    """Gets the index relative to the side."""
    if _sideLength == 1 :
        return 0
    else: 
        return _indexInLayer % (_sideLength - 1)

def _getSideIndexFromIndexAndLength(_indexInLayer, _sideLength):
    """Based on the length of sides in the layer and our index within it, return the side index."""
    if _sideLength == 1:
        return 0
    else:
        return int((_indexInLayer / (_sideLength - 1)) % 4)

def _getSideIndexFromLayerSignAndAxis(_sideIsPositive, _sideIsX):
    """Based on the sign of this side's coordinate and its component, return the side index."""
    if _sideIsPositive:
        if _sideIsX:
            return 0
        else:
            return 1
    else:
        if _sideIsX:
            return 2
        else:
            return 3
    return 0

if __name__ == "__main__":
    main()
